using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0f;
    [SerializeField]
    private float _rotateSpeed = 20f;

    [SerializeField]
    private GameObject _explosionAnimationPrefab;

    [SerializeField]
    private Spawner_script _spawner;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _rotateSpeed* Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Laser")
        {
            _spawner.StartSpawning();

            Destroy(other.gameObject);
            _speed = 0;
            GameObject explosion = Instantiate(_explosionAnimationPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
