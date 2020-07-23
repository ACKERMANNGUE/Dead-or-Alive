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
    public int getLayer() { 
        return nbLayer;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(Convert.ToInt32(collision.name));
        if (collision.gameObject.tag == TAG_LAYER
            && Convert.ToInt32(collision.gameObject.name) >= 1
            && Convert.ToInt32(collision.gameObject.name) <= 10)
        {
            nbLayer = Convert.ToInt32(collision.gameObject.name);
        }
    }

}
