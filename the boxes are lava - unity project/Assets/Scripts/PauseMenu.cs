using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject pauseButton;

    public GameObject resumeButton;

    [HideInInspector] public bool isGamePause = false;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isGamePause = true;
        pauseButton.SetActive(false);
        
        if (!playerController.isGameOver)
        {
            resumeButton.SetActive(true);
        }
        else
        {
            resumeButton.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        
        if (!playerController.isGameOver)
        {
            Time.timeScale = 1;
        }

        isGamePause = false;
        pauseButton.SetActive(true);
    }
}
