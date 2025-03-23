using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager_script : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _restartText;
    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private TMP_Text _bestScoreText;

    private int bestScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _livesImage.sprite = _sprites[3];
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        _bestScoreText.text = "Best: " + bestScore;
        UpdateLives(3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckForBestScore(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        _bestScoreText.text = "Best: " + bestScore; 
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
        CheckForBestScore(score);
    }

    public void UpdateLives(int lives) 
    {
        _livesImage.sprite = _sprites[Math.Max(lives,0)];
        if (lives <= 0)
        {
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerCoroutine());
        }
    }

    private IEnumerator GameOverFlickerCoroutine() 
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            _gameOverText.gameObject.SetActive(!_gameOverText.gameObject.activeSelf);
        }
    }
}
