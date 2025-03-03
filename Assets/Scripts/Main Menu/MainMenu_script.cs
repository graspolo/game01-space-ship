using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadOnePlayerGame()
    {
        SceneManager.LoadScene(1); //One Player Scene
    }
    public void LoadTwoPlayersGame()
    {
        SceneManager.LoadScene(2); //Two players Scene
    }
}
