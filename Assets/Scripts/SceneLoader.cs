using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(1);
        Debug.Log("GAME STARTED");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GAME QUIT");
    }
}
