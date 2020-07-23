using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rage : MonoBehaviour
{
    public float timeRage;
    public int dmg;
    public GameObject canvasRage;
    public Slider slider;
    public static bool rageEnded;
    public bool start;

    int timePassed;

    // Start is called before the first frame update
    void Start()
    {
        rageEnded = false;
        canvasRage = GameObject.Find("Canvas Rage");
        canvasRage.gameObject.SetActive(false);
        slider.maxValue = timeRage;

    }

    void Update()
    {
        if (start)
        {
            timeRage -= Time.deltaTime;
            SetRage(timeRage);
            if (timeRage < 0)
            {
                rageEnded = true;
            }
        }
    }


    public void SetRage(float timeRemaining)
    {
        slider.value = timeRemaining;
    }

    public void StartTimer()
    {
        start = true;
        canvasRage.gameObject.SetActive(true);
    }
}