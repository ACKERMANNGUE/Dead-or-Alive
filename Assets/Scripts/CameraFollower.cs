using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const int MIN = 0;
    private const int MAX = 100;
    public Transform target;
    public float speedSmooth = 10f;
    public Vector2 offset = new Vector2(0, 5);
    GameObject mainCamera;
    void Start()
    {
        mainCamera = (GameObject)GameObject.FindWithTag("MainCamera");
    }


    void LateUpdate()
    {
        Vector2 desiredPosition = (Vector2)target.position + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, speedSmooth * Time.deltaTime);
        mainCamera.transform.position = smoothedPosition;
    }
}
