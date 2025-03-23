using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private bool _isAlive = true;
    private List<Player_script> _players;
    private Animator _anim;
    private float _canFire = -1f;
    private float _fireRate = 3f;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private void Start()
    {
        _players = new List<Player_script>(GameObject.FindGameObjectsWithTag("Player")
            .Select(playerObject => playerObject.GetComponent<Player_script>())
            .Where(playerScript => playerScript != null));

        if (_players.Count == 0)
        {
            Debug.LogError("Players is empty!");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is null!");
        }
        _isAlive = true;
    }
    void Update()
    {
        CalculateMovement();
        Shoot();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.6)
        {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire && _isAlive)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser_script[] lasers = enemyLaser.GetComponentsInChildren<Laser_script>();
            foreach (Laser_script laser in lasers)
            {
                laser.AssignEnemyLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            _isAlive = false;
            Player_script player = other.transform.GetComponent<Player_script>();
            if (player != null)
            {
                player.TakeDamage();
                player.AddScore(50);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 2.37f);
        }
        else if (other.transform.tag == "Laser")
        {
            _isAlive = false;
            var enemyLaser = other.GetComponent<Laser_script>();
            if ( enemyLaser != null && !enemyLaser.IsEnemyLaser())
            {
                Destroy(other.gameObject);
                if (_players != null)
                {
                    _players.First().AddScore(10);
                }
                _anim.SetTrigger("OnEnemyDeath");
                _speed = 0;
                GetComponent<AudioSource>().Play();
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 2.37f);
            }
        }
    }
}
