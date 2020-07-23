using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    /// <summary>
    /// Permet de détruire une salle
    /// </summary>
    public void DestructRoom() {
        Destroy(gameObject);    
    }
}
