using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(2);
        Debug.Log("GAME STARTED");
    }

    public void StartDefenceMode()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(3);
    }

    public void LoadMenu()
    {
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GAME QUIT");
    }
}
