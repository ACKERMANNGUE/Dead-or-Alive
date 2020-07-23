using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public LayerMask IsItRoom;
    public LevelGeneration lvlGen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, IsItRoom);
        if (roomDetection == null && LevelGeneration.stopGeneration == true) {
            int rnd = Random.Range(0, lvlGen.rooms.Length);
            Instantiate(lvlGen.rooms[rnd], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
