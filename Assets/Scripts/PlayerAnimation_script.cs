using UnityEngine;

public class PlayerAnimation_script : MonoBehaviour
{
    private Animator _anim;
    private Player_script _playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerScript = GetComponent<Player_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerScript.GetPlayerNumber() == 1)
        {
            float moveDirection = Input.GetAxisRaw("Horizontal"); // -1 (sinistra), 1 (destra), 0 (fermo)
            _anim.SetFloat("Direction", moveDirection);
        }
        else
        {
            float moveDirection = Input.GetAxisRaw("Horizontal2"); // -1 (sinistra), 1 (destra), 0 (fermo)
            _anim.SetFloat("Direction", moveDirection);
        }
        
    }
}

