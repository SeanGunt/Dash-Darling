using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private GameObject reticle;
    private bool paused;

    private void Awake()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Resume();
    }
    public void OnPauseButtonClick()
    {
        paused = true;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        reticle.SetActive(false);
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        reticle.SetActive(true);
        Cursor.visible = false;
    }

    public void OnHover()
    {
        Cursor.visible = true;
        reticle.SetActive(false);
    }

    public void OnHoverExit()
    {
        if(!paused)
        {
            Cursor.visible = false;
            reticle.SetActive(true);
        }
        
        else
        {
            Cursor.visible = true;
            reticle.SetActive(false);
        }
    }
}
