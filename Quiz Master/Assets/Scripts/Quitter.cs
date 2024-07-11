using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quitter : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("SelectionScene", LoadSceneMode.Single);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        SelectionScreen.selectionLoaded = false;
        SelectionScreen.isSelected = false;


    }

}

