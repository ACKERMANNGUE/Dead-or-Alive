using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const int MIN = 0;
    private const int MAX = 100;
    GameObject mainCamera;
    void Start()
    {
        mainCamera = (GameObject)GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(transform.position.x, MIN, MAX);
        float y = Mathf.Clamp(transform.position.y, MIN, MAX);
        mainCamera.transform.position = new Vector2(x, y);
    }
}
