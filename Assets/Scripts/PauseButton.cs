using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private GameObject reticle;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        Resume();
    }
    public void OnPauseButtonClick()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        reticle.SetActive(false);
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        reticle.SetActive(true);
        Cursor.visible = false;
    }
}
