using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenControl : MonoBehaviour
{
    public GameObject loadingScreenObj;
    public Slider slider;
    /// <summary>
    /// Charge une scène
    /// </summary>
    /// <param name="sceneIndex">l'ID de la scène</param>
    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

   

}
