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
        inpSeed.onValueChanged.AddListener(delegate { OnChange(); });
        cnvMenu.gameObject.SetActive(true);
        cnvSeed.gameObject.SetActive(false);
        msgError.gameObject.SetActive(false);

    }
    public void DisplayMenuSeed() {
        cnvMenu.gameObject.SetActive(false);
        cnvSeed.gameObject.SetActive(true);
    }
    public void Play()
    {
        
        if (txtSeed.text.Length < 1)
        {
            msgError.gameObject.SetActive(true);
            msgError.text = "Veuillez rentrer minimum 1 nombre !";
            if (!tglSeedOrNot.isOn)
            {
                msgError.gameObject.SetActive(false);
                LevelGeneration.seed = NO_SEED;
            }
        }
        else
        {
            if (tglSeedOrNot.isOn)
            {
                LevelGeneration.seed = Convert.ToInt32(txtSeed.text);
            }
        }
        SceneManager.LoadScene("Game");
    }

    public void Exit() {
        Application.Quit();
    }

    public void OnChange()
    {
        if (txtSeed.text.Length > 0 && txtSeed.text.Length < 10)
        {
            msgError.gameObject.SetActive(false);
        }
        else
        {
            msgError.gameObject.SetActive(true);
        }
    }
}
