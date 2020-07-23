using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const string TAG_CENTER_ROOM = "Center Room";
    private const string TAG_ROOM_DETECTION = "Room Detection";
    private const string HEAL_CODE = "Heal";
    private const string RAGE_CODE = "Rage";
    private const string HOMEOSTASIS = "Homeostasis";
    private const string CROWN = "Crown";
    private const string END = "End";
    private const int ID_LAYER_PLAYER = 10;
    private const int ID_LAYER_ROOM = 8;
    private const int LEFT = -1;
    private const int RIGHT = 1;
    private const int IDLE = 0;

    public GameObject prefab;
    public Rigidbody2D rb;
    public Vector2 pos;
    public Animator anim;


    public Rage ragePlayer;
    public Health hpPlayer;
    public GameObject dmgUI;
    public int dmgPlayer;
    public LayerCounter layerCounter;
    public Text txtCrossedRooms;
    public Text txtFloor;
    public Text txtCrown;

    public int crossedRooms;
    public int floor;
    public int crowns;

    public float speed;
    public float jumpForce;
    private float moveInput;
    private float jumpTime = 2;
    public float jumpCounter;

    private bool isGrounded;
    private bool isOnWall;
    private bool isJumping;
    public Transform feetPos;
    public Transform handPos;
    public Transform headPos;
    public float checkRadius;
    public LayerMask groundType;

    private float orientation;

    public bool dmgHasChanged = false;

    public List<GameObject> lstDmgUi;

    public Enemies enemies;
    public Potions potions;
    public Items items;
    public List<GameObject> lstCrossedRooms;

    void Start()
    {
        /* Init */
        /* Lists */
        enemies = new Enemies();
        potions = new Potions();
        items = new Items();
        lstCrossedRooms = new List<GameObject>();
        lstDmgUi = new List<GameObject>();
        /* Composants */
        anim = GetComponent<Animator>();
        floor = GetComponent<LayerCounter>().nbLayer;
        rb = prefab.GetComponent<Rigidbody2D>();
        pos = prefab.transform.position;
        /* Var */
        jumpCounter = 0;
        crowns = 0;
        crossedRooms = 0;
        /* UI */
        DisplayDamages();
        DisplayCrowns();
        DisplayCrossedRooms();
        DisplayFloor();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput == LEFT)
        {
            anim.SetBool("IsRunning", true);
            orientation = 0;
            /* Retourne le sprite à son état d'origine */
            transform.rotation = Quaternion.Euler(0, orientation, 0);
        }
        else if (moveInput == RIGHT)
        {
            anim.SetBool("IsRunning", true);
            orientation = 180;
            /* Retourne le sprite de 180 degré (symmetrie) */
            transform.rotation = Quaternion.Euler(0, orientation, 0);
        }
        if(moveInput == IDLE)
        {
            anim.SetBool("IsRunning", false);
        }
        /* On le déplace */
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        floor = GetComponent<LayerCounter>().getLayer();
        crossedRooms = lstCrossedRooms.Count;
        /* S'il touche le sol avec ses pieds */
        if (isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundType))
        { 
            anim.SetBool("IsJumping", false);
        }
        /* S'il touche le mur avec ses mains */
        isOnWall = Physics2D.OverlapCircle(handPos.position, checkRadius + 0.2f, groundType);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            /* S'il touche le sol avec ses pieds */
            if (isGrounded)
            {
                /* Reset son nombre de saut actuel */
                anim.SetBool("IsGrounded", true);
                jumpCounter = 0;
            }
            /* Tant qu'il n'a pas atteint le nombre de jump max */
            if (jumpCounter != jumpTime)
            {
                Jump();
            }

        }
        /* S'il est sur le mur et qu'il essaie de s'y coller, il glisse */
        if (isOnWall && Input.GetAxisRaw("Horizontal") != 0)
        {
            isOnWall = Physics2D.OverlapCircle(headPos.position, checkRadius + 0.3f, groundType);
            /* Reset son nombre de saut actuel */
            jumpCounter = 0;
            /* On le force à glisser */
            rb.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
        }
        /* Si son nombre de dégâts a changé */
        if (dmgHasChanged)
        {
            /* On réaffiche le tout */
            ClearDamagesUI();
            DisplayDamages();
        }
        /* Si son effet de rage est terminé et que les dégâts n'ont pas encore changé */
        if (Rage.rageEnded && !dmgHasChanged)
        {
            /* On enlève le bonus de dégâts */
            dmgPlayer -= ragePlayer.dmg;
            dmgHasChanged = true;
        }
        /* Affichage constant du nombre de salles parcourues ainsi que de l'étage actuel */
        DisplayFloor();
        DisplayCrossedRooms();
    }
    /// <summary>
    /// Efface l'affichage des dégâts
    /// </summary>
    private void ClearDamagesUI()
    {
        for (int i = 0; i < lstDmgUi.Count; i++)
        {
            Destroy(lstDmgUi[i]);
        }
    }
    /// <summary>
    /// Affiche l'affichage des dégâts
    /// </summary>
    private void DisplayDamages()
    {
        GameObject dmgUICanvas = GameObject.FindGameObjectWithTag("DMG");
        /* Position initiale */
        float posX = dmgUI.transform.position.x;
        float posY = dmgUI.transform.position.y;
        for (int i = 0; i < dmgPlayer; i++)
        {
            GameObject dmg = Instantiate(dmgUI, new Vector2(posX + i * 15, posY), Quaternion.identity);
            dmg.transform.SetParent(dmgUICanvas.transform);
            lstDmgUi.Add(dmg);
        }
        dmgHasChanged = false;
    }
    /// <summary>
    /// Affiche le nombre de salles parcourures
    /// </summary>
    private void DisplayCrossedRooms()
    {
        txtCrossedRooms.text = "Crossed rooms : " + crossedRooms;
    }
    /// <summary>
    /// Affiche l'étage actuel
    /// </summary>
    private void DisplayFloor()
    {
        txtFloor.text = "Floor : " + floor;
    }
    /// <summary>
    /// Affiche le nombre de couronnes récupérées
    /// </summary>
    private void DisplayCrowns()
    {
        txtCrown.text = crowns.ToString();
    }
    /// <summary>
    /// Fait sauter le personnage
    /// </summary>
    private void Jump()
    {
        anim.SetBool("IsJumping", true);
        jumpCounter++;
        rb.velocity = Vector2.up * jumpForce;
    }

    //Just hit another collider 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Ignore les collision avec le centre de la salle, le bord de la salle ainsi que les différent layer qui servent de détection d'étage */
        Physics2D.IgnoreLayerCollision(ID_LAYER_PLAYER, ID_LAYER_ROOM);

        if (collision.gameObject.tag == TAG_CENTER_ROOM
            || collision.gameObject.tag == TAG_ROOM_DETECTION
            || collision.gameObject.tag == LayerCounter.TAG_LAYER)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        }
        /* Si la salle n'existe pas dans la liste des salles parcourures, on l'ajoute */
        if (collision.gameObject.tag == TAG_ROOM_DETECTION)
        {
            if (!lstCrossedRooms.Contains(collision.gameObject))
            {
                lstCrossedRooms.Add(collision.gameObject);
            }
        }
        /* Take damages */
        if (enemies.dctEnemies.Count > 0)
        {
            if (hpPlayer != null && enemies.dctEnemies.ContainsKey(collision.gameObject.tag))
            {
                hpPlayer.TakeDamages(Convert.ToInt32(enemies.dctEnemies[collision.gameObject.tag]));
            }
        }
        /* Vérifie que les potions existent */
        if (potions.dctPotions.Count > 0)
        {
            if (hpPlayer != null && potions.dctPotions.ContainsKey(collision.gameObject.tag))
            {
                /* Heal */
                string strHeal = collision.gameObject.tag.Substring(0, 4);
                if (strHeal == HEAL_CODE)
                {
                    hpPlayer.RegenHP(Convert.ToInt32(potions.dctPotions[collision.gameObject.tag]));
                    Destroy(collision.gameObject);
                }
                /* Rage */
                string strRage = collision.gameObject.tag.Substring(0, 4);
                if (strRage == RAGE_CODE)
                {
                    ragePlayer.dmg = Convert.ToInt32(potions.dctPotions[collision.gameObject.tag]);
                    ragePlayer.StartTimer();
                    dmgPlayer += ragePlayer.dmg;
                    Destroy(collision.gameObject);
                    dmgHasChanged = true;
                }
            }
        }
        /* Vérifie que les items existent */
        if (items.dctItems.Count > 0)
        {
            if (items.dctItems.ContainsKey(collision.gameObject.tag))
            {
                if (collision.gameObject.tag == CROWN)
                {
                    crowns++;
                    DisplayCrowns();
                    Destroy(collision.gameObject);
                }
                if (collision.gameObject.tag == HOMEOSTASIS)
                {
                    dmgPlayer = hpPlayer.health;
                    dmgHasChanged = true;
                    Destroy(collision.gameObject);
                }
            }
        }
        /* S'il rentre en contact avec la porte il passe au niveau suivant */
        if (collision.gameObject.tag == END)
        {
            LevelGeneration.stopTimer = true;
        }
    }
}
