using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{
    public static bool isSelected = false;
    public static bool selectionLoaded = false;
    public static int difficulty;
    public static int category = 0;
    private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {   
        if (!selectionLoaded)
        {
            SceneManager.LoadScene("SelectionScene", LoadSceneMode.Single);
            selectionLoaded = true;
        }
    }

    public void OnCategorySelected(int index)
    {
        category = index;
    }

    public void OnDifficultySelected(int index)
    {
        difficulty = index;
        isSelected = true;
        Quiz.quizInitialized = true;

        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
