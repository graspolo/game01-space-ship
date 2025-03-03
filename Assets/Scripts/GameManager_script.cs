using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_script : MonoBehaviour
{

    public bool isGameOver = false;
    public bool isCoOpMode = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            if (isCoOpMode == true)
            {
                SceneManager.LoadScene(2); //Co-Op Game Scene
            }
            else
            {
                SceneManager.LoadScene(1); //Main Game Scene
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0); //Main Menu Scene
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
