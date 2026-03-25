using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // Components 
    private Rigidbody2D _rigidbody2D;

    // Forces 
    public float jumpForce = 10;
    public float fallForce = 2;
    private Vector2 _gravityVector;

    // Capsule
    public float CapsuleHeight = 0.25f;
    public float CapsuleRadius = 0.08f;

    // Water Check 
    public bool _waterCheck;
    private string _waterTag = "Water";

    // Ground Check 
    public Transform feetCollider;
    public LayerMask groundMask;
    private bool _groundCheck;

    // Sets gravity vector and connects components 
    void Start() {
        _gravityVector = new Vector2(0, -Physics2D.gravity.y);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if player is touching ground 
        _groundCheck = Physics2D.OverlapCapsule(feetCollider.position, new Vector2(CapsuleHeight, CapsuleRadius), CapsuleDirection2D.Horizontal, 0, groundMask);

        // Checks if player is trying to jump/can jump 
        if (Input.GetKeyDown(KeyCode.Space) && (_groundCheck || _waterCheck))
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpForce);
        }

        // Checks if the gravity should be getting faster 
        if(_rigidbody2D.linearVelocity.y < 0 && !_waterCheck)
        {
            _rigidbody2D.linearVelocity -= _gravityVector * (fallForce * Time.deltaTime);
        }
    }

    // Checks if player is in water 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _waterTag)
        {
            _waterCheck = true;
        }
    }

    // Check if player left water 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _waterTag)
        {
            _waterCheck = false;
        }
    }
}
