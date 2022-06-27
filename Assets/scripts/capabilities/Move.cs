using System.Collections;
using System.Collections.Generic;
using Assets.scripts.checks;
using Assets.scripts.controllers;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float maxAcceleration = 20f;
    [SerializeField] private float maxAirAceeleration = 14f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        acceleration = onGround ? maxAcceleration : maxAirAceeleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;
    }
    
    
}
