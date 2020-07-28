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
        /* Init */
        rageEnded = false;
        canvasRage = GameObject.Find("Canvas Rage");
        if (canvasRage != null)
        {
            canvasRage.gameObject.SetActive(false);
        }
        slider.maxValue = timeRage;
        start = false;
    }

    void Update()
    {
        if (start)
        {
            /* Tant que l'effet de rage n'est pas fini on l'affiche et le décremente sur la durée */
            timeRage -= Time.deltaTime;
            SetRage(timeRage);
            if (timeRage < 0)
            {
                rageEnded = true;
                canvasRage.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Remplis la barre de duration avec le temps restant
    /// </summary>
    /// <param name="timeRemaining">Le temps restant</param>
    public void SetRage(float timeRemaining)
    {
        slider.value = timeRemaining;
    }
    /// <summary>
    /// On démarre le timer
    /// </summary>
    public void StartTimer()
    {
        start = true;
        canvasRage.gameObject.SetActive(true);
    }
}