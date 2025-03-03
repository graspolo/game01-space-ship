using Unity.VisualScripting;
using UnityEngine;

public class Laser_script : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private bool _isEnemyLaser = false;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 7)
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _speed *= -1;
        _isEnemyLaser = true;
    }

    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser)
        {
            Player_script player = other.GetComponent<Player_script>();
            if (player != null)
            {
                player.TakeDamage();
            }
            Destroy(this.gameObject);
        }
    }
}
