using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_script : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private List<GameObject> _powerups;

    [SerializeField]
    private GameManager_script _gameManager;

    private bool _stopSpawning = false;

    // Update is called once per frame
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager_script>();

    }


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(SpawnPowerupCoroutine());
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false && _gameManager.isGameOver == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);

        }
    }

    IEnumerator SpawnPowerupCoroutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false && _gameManager.isGameOver == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_powerups[Random.Range(0,3)], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3f, 7f));

        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
