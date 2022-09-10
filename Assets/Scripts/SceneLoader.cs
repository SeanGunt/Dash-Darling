using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("GAME STARTED");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GAME QUIT");
    }
}
