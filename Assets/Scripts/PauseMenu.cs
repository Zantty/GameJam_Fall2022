using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject optionsMenuPanel;
    [SerializeField] private GameObject gameUI;

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        gameUI.SetActive(false);

        Time.timeScale = 0;
    }
    public void UnpauseGame()
    {
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);
        gameUI.SetActive(true);

        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale < 0.1f)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
