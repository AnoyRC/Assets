using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator _animator;
    Movement _movement;
    // Start is called before the first frame update
    void Start()
    {
        _animator  = GetComponent<Animator>();
        _movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_movement.HasStarted) return;
        if( _movement.IsDead)
        {
            _animator.SetBool("isMoving", false);
            return;
        }
        if (_movement.IsGrounded(0.51f))
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }
}
