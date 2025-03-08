using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_script : MonoBehaviour
{

    public bool isGameOver = false;
    public bool isCoOpMode = false;
    private bool _isGamePaused = false;
    private bool _isEasterEggActive = false;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private Animator _pauseMenuAnim;

    [SerializeField]
    private GameObject _easterEgg;

    private void Start()
    {
        _isGamePaused = false;
        _pauseMenuAnim = _pauseMenuPanel.GetComponent<Animator>();  
        _pauseMenuAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
        _easterEgg.SetActive(false);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (isCoOpMode == true)
                {
                    SceneManager.LoadScene(2); //Co-Op Game Scene
                }
                else
                {
                    SceneManager.LoadScene(1); //Main Game Scene
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoToMainMenu(); //Main Menu Scene
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            PlayEasterEgg();
        }   
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void PauseGame()
    {
        if (_isGamePaused == false)
        {
            _pauseMenuPanel.SetActive(true);
            _isGamePaused = true;
            _pauseMenuAnim.SetBool("isPaused", true);
            Time.timeScale = 0f;
        }
        else
        {
            _pauseMenuPanel.SetActive(false);
            _pauseMenuAnim.SetBool("isPaused", false);
            _isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PlayEasterEgg()
    {
        _isEasterEggActive = !_isEasterEggActive;
        _easterEgg.SetActive(_isEasterEggActive);
    }
}
