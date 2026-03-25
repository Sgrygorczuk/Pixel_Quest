using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private const string _isWalking = "isWalking"; 


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody2D.linearVelocity.x != 0)
        {
            _animator.SetBool(_isWalking, true);
        }
        else
        {
            _animator.SetBool(_isWalking, false);
        }
    }
}
