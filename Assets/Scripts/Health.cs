using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MilkShake;

public class Health : MonoBehaviour
{
    public Shaker myShaker;
    public ShakePreset preset;
    public int health;
    public int maxHealth;
    public int minHealth;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
        health = maxHealth;
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    public void SetHealth(int hp)
    {
        slider.value = hp;
    }

    public void TakeDamages(int amount)
    {
        health -= amount;
        myShaker.Shake(preset);
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        SetHealth(health);

    }

    public void RegenHP(int amount)
    {
        health += amount;
        SetHealth(health);
    }
}
