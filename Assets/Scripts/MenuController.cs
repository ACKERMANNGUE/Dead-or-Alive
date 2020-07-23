using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    const int NO_SEED = -1;

    public Text msgError;
    public Canvas cnvMenu;
    public Canvas cnvSeed;
    public InputField inpSeed;
    public Text txtSeed;
    public Toggle tglSeedOrNot;
    //1788218204

    private void Start()
    {
        /* Création d'event pour input text pour la seed */
        inpSeed.onValueChanged.AddListener(delegate { OnChange(); });
        /* Init */
        cnvMenu.gameObject.SetActive(true);
        cnvSeed.gameObject.SetActive(false);
        msgError.gameObject.SetActive(false);

    }
    /// <summary>
    /// Affiche la fenêtre pour rentrer une seed
    /// </summary>
    public void DisplayMenuSeed() {
        cnvMenu.gameObject.SetActive(false);
        cnvSeed.gameObject.SetActive(true);
    }
    /// <summary>
    /// Récupère les informations saisies par l'utilisateur et lance la partie
    /// </summary>
    public void Play()
    {
        /* Vérification d'input */
        if (txtSeed.text.Length < 1)
        {
            msgError.gameObject.SetActive(true);
            msgError.text = "Veuillez rentrer minimum 1 nombre !";
            /* si la case à cochée ne l'est pas */
            if (!tglSeedOrNot.isOn)
            {
                /* On dit au script de génération qu'aucune seed n'est utilisée */
                msgError.gameObject.SetActive(false);
                LevelGeneration.seed = NO_SEED;
            }
        }
        else
        {
            /* si la case à cochée l'est*/
            if (tglSeedOrNot.isOn)
            {
                /* On donne au script de génération seed rentrée par l'utilisateur */
                LevelGeneration.seed = Convert.ToInt32(txtSeed.text);
            }
        }
        SceneManager.LoadScene("Game");
    }
    /// <summary>
    /// Quitte l'application
    /// </summary>
    public void Exit() {
        Application.Quit();
    }
    /// <summary>
    /// Quand la valeur change on vérifie qu'il soit bien supérieur à 0
    /// </summary>
    public void OnChange()
    {
        if (txtSeed.text.Length > 0)
        {
            msgError.gameObject.SetActive(false);
        }
        else
        {
            msgError.gameObject.SetActive(true);
        }
    }
}
