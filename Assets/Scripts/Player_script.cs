using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shield;
    private GameManager_script _gameManager;

    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _laserSFX;

    [SerializeField]
    private int _playerNumber = 1;

    [SerializeField]
    private BoxCollider2D _collider;

    private int score = 0;

    [SerializeField]
    private UI_Manager_script _uiManagerScript;

    private Dictionary<PowerupEnum, Coroutine> _activePowerdowns = new();

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager_script>();
        _collider = GetComponent<BoxCollider2D>();
        _leftEngine.gameObject.SetActive(false);
        _rightEngine.gameObject.SetActive(false);

        if (_gameManager.isCoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }else if (_playerNumber == 1)
        {
            transform.position = new Vector3(-2, 0, 0);
        }
        else if (_playerNumber == 2)
        {
            transform.position = new Vector3(2, 0, 0);
        }

        _shield.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Player audio source is null");
        }
        else
        {
            _audioSource.clip = _laserSFX;
        }
    }

    void Update()
    {
        if (_playerNumber == 1)
        {
            CalculateMovementPlayer1();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                ShootPlayer1();
            }
        }
        else if (_playerNumber == 2)
        {
            CalculateMovementPlayer2();
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > _canFire)
            {
                ShootPlayer2();
            }
        }

    }

    private void CalculateMovementPlayer1()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * (_speed + (Convert.ToInt32(_isSpeedActive) * 5)) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        else if (transform.position.x < -11.3f)
            transform.position = new Vector3(11.3f, transform.position.y, 0);
    }

    private void ShootPlayer1()
    {
        Vector3 offset = new Vector3(0, 1.05f, 0);
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
        _audioSource.Play();
    }

    private void CalculateMovementPlayer2()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        float verticalInput = Input.GetAxis("Vertical2");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * (_speed + (Convert.ToInt32(_isSpeedActive) * 5)) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        else if (transform.position.x < -11.3f)
            transform.position = new Vector3(11.3f, transform.position.y, 0);
    }

    private void ShootPlayer2()
    {
        Vector3 offset = new Vector3(0, 1.05f, 0);
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void TakeDamage()
    {
        if (_isShieldActive == false)
        {
            _collider.enabled = false;

            this._lives--;
            if (this._lives == 2)
                _rightEngine.gameObject.SetActive(true);
            if (this._lives == 1)
                _leftEngine.gameObject.SetActive(true);

            _uiManagerScript.UpdateLives(_lives);
            StartCoroutine(TakeDamageFlickerCoroutine());
            if (this._lives <= 0)
            {
                _gameManager.GameOver();
                Destroy(this.gameObject);
            }
        }
        else
        {
            _shield.SetActive(false);
            _isShieldActive = false;
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        RestartPowerdownCoroutine(PowerupEnum.TripleShot);
    }

    public void ActivateSpeed()
    {
        _isSpeedActive = true;
        RestartPowerdownCoroutine(PowerupEnum.Speed);
    }

    public void ActivateShield()
    {
        _shield.SetActive(true);
        _isShieldActive = true;
    }


    private void RestartPowerdownCoroutine(PowerupEnum powerupId)
    {
        if (_activePowerdowns.TryGetValue(powerupId, out Coroutine existingCoroutine))
        {
            StopCoroutine(existingCoroutine);
        }

        Coroutine newCoroutine = StartCoroutine(PowerdownCoroutine(powerupId));
        _activePowerdowns[powerupId] = newCoroutine;
    }

    public void AddScore(int newScore)
    {
        score = score + newScore;
        _uiManagerScript.UpdateScore(this.score);
    }

    public int GetScore()
    {
        return score;
    }

    public IEnumerator PowerdownCoroutine(PowerupEnum powerupId)
    {
        yield return new WaitForSeconds(5.0f);

        switch (powerupId)
        {
            case PowerupEnum.TripleShot:
                _isTripleShotActive = false;
                break;

            case PowerupEnum.Speed:
                _isSpeedActive = false;
                break;

            case PowerupEnum.Shield:
                _isShieldActive = false;
                break;

        }

        _activePowerdowns.Remove(powerupId);


    }

    private IEnumerator TakeDamageFlickerCoroutine()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float flickerDuration = 0.5f;
        float flickerInterval = 0.1f;
        int flickerCount = Mathf.RoundToInt(flickerDuration / flickerInterval);

        for (int i = 0; i < flickerCount; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds(flickerInterval);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(flickerInterval);
        }

        _collider.enabled = true;
    }

    public int GetPlayerNumber() 
    {
        return _playerNumber; 
    }
}
