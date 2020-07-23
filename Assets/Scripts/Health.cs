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

   
    void Start()
    {
        /* Init */
        health = maxHealth;
        slider.maxValue = maxHealth;
    }

    /// <summary>
    /// Change la valeur du slider au nombre d'HP
    /// </summary>
    /// <param name="hp">Les Point de vies du joueur</param>
    public void SetHealth(int hp)
    {
        slider.value = hp;
    }
    /// <summary>
    /// Fait subir des dégâts au joueur
    /// </summary>
    /// <param name="amount">Montant de dégâts reçu</param>
    public void TakeDamages(int amount)
    {
        health -= amount;
        /* Animation Shake*/
        myShaker.Shake(preset);
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        SetHealth(health);

    }
    /// <summary>
    /// Fait regagner de la vie au joueur
    /// </summary>
    /// <param name="amount">Montant de points de vie reçu</param>
    public void RegenHP(int amount)
    {
        health += amount;
        SetHealth(health);
    }
}
