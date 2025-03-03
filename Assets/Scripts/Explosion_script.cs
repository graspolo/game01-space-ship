using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource explosion = GetComponent<AudioSource>();
        explosion.Play();
        Destroy(this.gameObject, 2.58f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
