using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCounter : MonoBehaviour
{
    public const string TAG_LAYER = "Layer";
    public int nbLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// Retourne le numéro de l'étage actuel
    /// </summary>
    /// <returns>Le numéro de l'étage actuel</returns>
    public int getLayer() { 
        return nbLayer;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*S'il rentre en collision avec trigger sur un layer d'étage on change la valeur de l'étage */
        if (collision.gameObject.tag == TAG_LAYER
            && Convert.ToInt32(collision.gameObject.name) >= 1
            && Convert.ToInt32(collision.gameObject.name) <= 10)
        {
            nbLayer = Convert.ToInt32(collision.gameObject.name);
        }
    }

}
