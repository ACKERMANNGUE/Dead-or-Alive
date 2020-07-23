using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGeneration : MonoBehaviour
{
    const float OFFSET_POS = 5.1f;
    const int RIGHT_ONE = 1;
    const int RIGHT_TWO = 2;
    const int LEFT_ONE = 3;
    const int LEFT_TWO = 4;
    const int BOTTOM = 5;

    const int ID_LR = 0;
    const int ID_LRB = 1;
    const int ID_LRBT = 2;
    const int ID_LRT = 3;

    const int NO_SEED = -1;
    public GameObject ui;
    public GameObject loadingScreen;
    public Slider slider;
    public Text txtSeedLoadingScreen;
    public Text txtSeed;

    public Transform[] startingPos;
    public GameObject[] rooms;
    public float moveAmount;
    public float timeBtwCreation;
    public float startTimeBtwCreation = 0.1f;
    public float minX;
    public float maxX;
    public float maxY;
    public float timer;
    public static bool stopTimer;
    public static bool stopGeneration;
    public Text txtTimer;

    public int DownCounter;
    public LayerMask room;
    public static int seed;
    private int direction;
    int oldDir;
    public GameObject Player;
    public GameObject End;
    public List<GameObject> lstPath;
    public static List<GameObject> lstEnemies;

    private bool posSet;
    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
        loadingScreen.SetActive(true);
        timer = 0;
        lstPath = new List<GameObject>();
        lstEnemies = new List<GameObject>();

        Player.SetActive(false);
        oldDir = -1;
        DownCounter = 0;
        if (seed == NO_SEED)
        {
            seed = Random.Range(0, int.MaxValue);
            Random.seed = seed;
            Debug.Log(string.Format("Seed : {0}", Random.seed));
        }
        else
        {
            Random.seed = seed;
            Debug.Log(string.Format("Seed : {0}", Random.seed));
        }
        txtSeedLoadingScreen.text = "Chargement du monde\n" + seed;
        txtSeed.text = seed.ToString();

        stopGeneration = false;

        int randStartPos = Random.Range(0, startingPos.Length);
        transform.position = startingPos[randStartPos].position;
        posSet = false;
        lstPath.Add(Instantiate(rooms[Random.Range(0, rooms.Length)],
            new Vector3(transform.position.x, transform.position.y, transform.position.z - 1),
            Quaternion.identity));

        oldDir = direction;
        direction = Random.Range(RIGHT_ONE, BOTTOM + 1);
    }

    void Update()
    {
        if (stopGeneration && !posSet)
        {
            Player.SetActive(true);
            Player.transform.position = lstPath[0].transform.position;
            posSet = true;
            ui.SetActive(true);
            loadingScreen.SetActive(false);

            foreach (GameObject g in LevelGeneration.lstEnemies)
            {
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minX, maxY);
                float rad = 5f;
                if (!Physics2D.OverlapCapsule(new Vector2(x, y), new Vector2(rad, rad), CapsuleDirection2D.Vertical, 90))
                {
                    GameObject go = (GameObject)Instantiate(g, new Vector2(x, y), Quaternion.identity);
                    go.transform.parent = g.transform;
                }
                else {
                    x = Random.Range(minX, maxX);
                    y = Random.Range(minX, maxY);
                    GameObject go = (GameObject)Instantiate(g, new Vector2(x + 5, y + 5), Quaternion.identity);
                    go.transform.parent = g.transform;
                }
            }
        }
        else if (!stopGeneration)
        {
            slider.value += 0.01f;
            if (stopGeneration)
            {
                slider.value += 1.9f;
                loadingScreen.SetActive(true);
            }
        }
        if (posSet && !stopTimer)
        {
            timer += Time.deltaTime;
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");
            txtTimer.text = string.Format("{0}:{1}", minutes, seconds);
        }
        else
        {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");
            txtTimer.text = string.Format("{0}:{1}", minutes, seconds);
        }
        if (timeBtwCreation <= 0)
        {
            Move();
            timeBtwCreation = startTimeBtwCreation;
        }
        else
        {
            timeBtwCreation -= Time.deltaTime;
        }


    }

    void Move()
    {
        if (!stopGeneration)
        {

            if (direction == RIGHT_ONE || direction == RIGHT_TWO)
            {
                if (transform.position.x + OFFSET_POS < maxX)
                {
                    DownCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rndRoom = Random.Range(0, rooms.Length);
                    lstPath.Add(Instantiate(rooms[rndRoom], transform.position, Quaternion.identity));
                    oldDir = direction;

                    direction = Random.Range(RIGHT_ONE, BOTTOM + 1);
                    if (direction == LEFT_ONE)
                    {
                        direction = RIGHT_ONE;
                    }
                    if (direction == LEFT_TWO)
                    {
                        direction = BOTTOM;
                    }
                }
                else
                {
                    direction = BOTTOM;
                }
            }
            if (direction == LEFT_ONE || direction == LEFT_TWO)
            {
                if (transform.position.x - OFFSET_POS > minX)
                {
                    DownCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rndRoom = Random.Range(0, rooms.Length);
                    lstPath.Add(Instantiate(rooms[rndRoom], transform.position, Quaternion.identity));
                    oldDir = direction;

                    direction = Random.Range(RIGHT_ONE, BOTTOM + 1);

                    if (direction == RIGHT_ONE)
                    {
                        direction = LEFT_ONE;
                    }
                    if (direction == RIGHT_TWO)
                    {
                        direction = BOTTOM;
                    }
                }
                else
                {
                    direction = BOTTOM;
                }
            }
            if (direction == BOTTOM)
            {
                DownCounter++;
                int rndRoomBot;

                if (transform.position.y > minX + OFFSET_POS)
                {
                    /* Detect la salle*/
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1f, room);
                    //Debug.LogWarning(string.Format("{0}", roomDetection.name));
                    /*Check que la salle n'aie pas d'ouverture basse */
                    if (roomDetection.GetComponent<RoomType>().type != ID_LRB && roomDetection.GetComponent<RoomType>().type != ID_LRBT)
                    {
                        if (DownCounter >= 2)
                        {
                            roomDetection.GetComponent<RoomType>().DestructRoom();
                            lstPath.RemoveAt(lstPath.Count - 1);
                            lstPath.Add(Instantiate(rooms[ID_LRBT], transform.position, Quaternion.identity));
                        }
                        else
                        {
                            /*Config de la salle précédente */
                            roomDetection.GetComponent<RoomType>().DestructRoom();
                            lstPath.RemoveAt(lstPath.Count - 1);
                            rndRoomBot = Random.Range(ID_LRB, ID_LRBT + 1);
                            lstPath.Add(Instantiate(rooms[rndRoomBot], transform.position, Quaternion.identity));
                        }
                        oldDir = direction;
                    }
                    else
                    {
                        /*Déplacement de la case */
                        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                        transform.position = newPos;
                        /* next room doit avoir une ouverture haute */
                        int rnd = Random.Range(ID_LRBT, ID_LRT + 1);
                        lstPath.Add(Instantiate(rooms[rnd], transform.position, Quaternion.identity));
                        oldDir = direction;
                        direction = Random.Range(1, 6);
                    }

                    /*On change de direction */

                }
                else
                {
                    //Level ended
                    lstPath.Add(Instantiate(End, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity));
                    stopGeneration = true;
                }

            }
        }
    }
}