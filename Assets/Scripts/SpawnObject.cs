using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private const int LAYER_ENEMY = 13;
    public GameObject[] objects;

    void Start()
    {
        int rnd = Random.Range(0, objects.Length);
       
        if (objects[rnd].gameObject.layer == LAYER_ENEMY)
        {
            GameObject go = objects[rnd];
            go.transform.position = transform.position;
            LevelGeneration.lstEnemies.Add(go);
        }
        else {
            GameObject go = (GameObject)Instantiate(objects[rnd], transform.position, Quaternion.identity);
            go.transform.parent = transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
