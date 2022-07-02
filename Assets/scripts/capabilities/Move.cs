using System.Collections;
using System.Collections.Generic;
using Assets.scripts.checks;
using Assets.scripts.controllers;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    
    private Rigidbody2D _body;
    private Ground _ground;
    private bool _onGround;

    // ========= WALK
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private float _maxAcceleration = 20f;
    [SerializeField] private float _maxAirAcceleration = 14f;

    private Vector2 _direction;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;

    private float _maxSpeedChange;
    private float _acceleration;


    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = _controller.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.GetFriction(), 0);
    }

    void FixedUpdate()
    {
        _onGround = _ground.GetOnGround();
        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);
        _velocity.y = _body.velocity.y;
        
        _body.velocity = _velocity;
    }
}
