using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float speed = 2;
    private int direction = 1;

    // Capsule
    public float CapsuleHeight = 0.25f;
    public float CapsuleRadius = 0.08f;

    // Ground Check 
    public Transform feetCollider;
    public LayerMask groundMask;
    private bool _groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.linearVelocity = new Vector2(speed * direction, _rigidbody2D.linearVelocity.y);

        // Checks if player is touching ground 
        _groundCheck = Physics2D.OverlapCapsule(feetCollider.position, new Vector2(CapsuleHeight, CapsuleRadius), CapsuleDirection2D.Horizontal, 0, groundMask);

        if (!_groundCheck)
        {
            direction *= -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= -1;
        transform.localScale = new Vector3(direction, 1, 1);
    }
}
