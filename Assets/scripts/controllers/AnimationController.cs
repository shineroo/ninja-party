using System.Collections;
using System.Collections.Generic;
using Assets.scripts.capabilities;
using Assets.scripts.checks;
using Assets.scripts.controllers;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    private Ground _ground;
    private Rigidbody2D _body;
    private Animator _animator;
    
    private Vector2 _direction;
    [SerializeField]private bool facingRight = true;
    private bool _onGround;
    private bool _attacking;
    private bool _jumping;

    [SerializeField] private string _currentState = Idle;
    // Animation states
    private const string Idle = "idle";
    private const string Run = "run";
    private const string Jump = "jump";
    private const string Fall = "fall";
    private const string DoubleJump1 = "doublejump1";
    private const string DoubleJump2 = "doublejump2";
    private const string Attack1 = "attack1";
    private const string Attack2 = "attack2";
    private const string Throw = "throw";
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = _controller.RetrieveMoveInput();
        _onGround = _ground.GetOnGround();
        _jumping |= _controller.RetrieveJumpInput();
    }

    void FixedUpdate()
    {
        // Flip player
        if (_direction.x < 0 && facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);   
                // this is wrong if you ever want to scale your ninjas
            facingRight = false;
        }
        if (_direction.x > 0 && !facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        
        // Run and Idle
        if (_onGround && _body.velocity.y <= 0)
        {
            if (_direction.x != 0)
            {
                ChangeAnimationState(Run);
            }
            if (_direction.x == 0)
            {
                ChangeAnimationState(Idle);
            }
        }

        // Jump
        if (_jumping)
        {
            if (!_onGround)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        _animator.Play(DoubleJump1); 
                        _currentState = DoubleJump1;
                        break;
                    case 2:
                        _animator.Play(DoubleJump2);
                        _currentState = DoubleJump2;
                        break;
                }
            }
            else
            {
                ChangeAnimationState(Jump);
            }
            _jumping = false;
        }

        if (!_onGround && _body.velocity.y < -0.1) 
            // -0.1 cause it freaks out when you touch walls if i leave it on 0
        {
            ChangeAnimationState(Fall); 
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) return;
        
        _animator.Play(newState);
        _currentState = newState;
    }
}
