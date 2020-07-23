using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    private const string TAG_CENTER_ROOM = "Center Room";
    private const string TAG_ROOM_DETECTION = "Room Detection";
    private const int ID_BRIDGE = 11;

    public int speed;
    public int detectPlayer;
    public int rangeToAtk;
    public LayerMask player;
    public Rigidbody2D rb;
    public GameObject[] cols;
    public List<GameObject> lstCols;

    public enum states { Idle, Moving, Combat }
    public states actualState;
    public void ChangeState(states st)
    {
        actualState = st;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lstCols = cols.OfType<GameObject>().ToList();
        ChangeState(states.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectPlayer, player);
        if (col.gameObject.layer == 10)
        {
            ChangeState(states.Moving);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, col.gameObject.transform.position, step);

            bool rangeOk = Physics2D.OverlapCircle(transform.position, rangeToAtk, player);
            if (rangeOk)
            {
                ChangeState(states.Combat);
                Debug.Log("Prend GARDE !");
            }
        }
        else { 
            ChangeState(states.Idle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TAG_CENTER_ROOM || collision.gameObject.tag == TAG_ROOM_DETECTION || collision.gameObject.tag == LayerCounter.TAG_LAYER)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }


        //Debug.LogWarning(col.tag);
        bool rangeOk = Physics2D.OverlapCircle(transform.position, 1f, ID_BRIDGE);
        if (rangeOk)
        {
            rb.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }

    }
}
