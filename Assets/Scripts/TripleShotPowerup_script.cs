using Assets.Scripts.Enums;
using UnityEngine;

public class TripleShotPowerup_script : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private PowerupEnum _powerupID;


    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player_script player = other.transform.GetComponent<Player_script>();
            if (player != null)
            {
                 
                switch (_powerupID)
                {
                    case PowerupEnum.TripleShot:
                        player.ActivateTripleShot();
                        break;

                    case PowerupEnum.Speed:
                        player.ActivateSpeed();
                        break;

                    case PowerupEnum.Shield:
                        player.ActivateShield();
                        break;

                }
            }
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            Destroy(this.gameObject);
        }
    }
}
