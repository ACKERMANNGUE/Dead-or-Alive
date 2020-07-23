using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private const int LAYER_ENEMY = 13;
    public GameObject[] objects;

    void Start()
    {
        /* On choisit un GameObject au hasard parmis ceux dispo dans l'éditeur */
        int rnd = Random.Range(0, objects.Length);
       /* Si c'est un ennemi on l'ajoute à la liste des ennemis à spawn plus tard*/
        if (objects[rnd].gameObject.layer == LAYER_ENEMY)
        {
            GameObject go = objects[rnd];
            go.transform.position = transform.position;
            LevelGeneration.lstEnemies.Add(go);
        }
        else {
            /* Sinon on spawn l'object (cela peut être : un item, un morceau de mur, une potion ou un pont */
            GameObject go = (GameObject)Instantiate(objects[rnd], transform.position, Quaternion.identity);
            go.transform.parent = transform;
        }
        
    }
}
