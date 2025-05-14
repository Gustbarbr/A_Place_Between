using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadSceneAsync("Opening");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
