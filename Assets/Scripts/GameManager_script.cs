using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_script : MonoBehaviour
{

    public bool isGameOver = false;
    public bool isCoOpMode = false;
    private bool _isGamePaused = false;

    [SerializeObject]
    private GameObject _pauseMenuPanel;

    private void Start()
    {
        _isGamePaused = false;
    }

    private void Update()
    {
        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R)) {
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
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    //TODO: invocare questo dal pannello di pausa
    public void PauseGame()
    {
        if (_isGamePaused == false)
        {
            //TODO: implementare un oggetto UI Pause Menù
            _pauseMenuPanel.SetActive(true);
            _isGamePaused = true;
            Time.TimeScale = 0f;
        }
        else
        {
            //TODO: implementare un oggetto UI Pause Menù
            _pauseMenuPanel.SetActive(false);
            _isGamePaused = false;
            Time.timeScale = 1f;
        }
    }
    
    //TODO: invocare questo dal pannello di pausa
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
