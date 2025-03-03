using UnityEngine;

public class PlayerAnimation_script : MonoBehaviour
{
    private Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal"); // -1 (sinistra), 1 (destra), 0 (fermo)
        _anim.SetFloat("Direction", moveDirection);
    }

    private void animatePlayerOne()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _anim.SetInteger("Move", 1);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            _anim.SetInteger("Move", 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _anim.SetInteger("Move", 2);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            _anim.SetInteger("Move", 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _anim.SetInteger("Move", 3);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _anim.SetInteger("Move", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _anim.SetInteger("Move", 4);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _anim.SetInteger("Move", 0);
        }
    }
}

