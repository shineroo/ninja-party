using System.Collections;
using System.Collections.Generic;
using Assets.scripts.capabilities;
using Assets.scripts.checks;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Move _move;
    private Jump _jump;
    private Attack _attack;
    private Rigidbody2D _body;
    private Ground _ground;
    private bool _stunned;
    private bool _dead;
    
    [SerializeField]private float _respawnSpeed;
    private float _respawnTimer;

    private bool _onGround;
    
    [SerializeField, Range(0f, 10f)] private float _launchHeight = 3f;
    [SerializeField, Range(0f, 10f)] private float _launchLength = 3f;

    [SerializeField] private GameObject _hiteffect;
    
    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponent<Move>();
        _jump = GetComponent<Jump>();
        _attack = GetComponent<Attack>();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate()
    { 
        _onGround = _ground.GetOnGround();
        if (_stunned && _onGround && _body.velocity.y <= 0.1) 
            // _body.velocity.y <= 0.1 because i guess rigidbody2d doesnt always go back to exactly 0
        {
            _stunned = false;
            /*_move.enabled = true;
            _jump.enabled = true;
            _attack.enabled = true;*/
            _body.velocity = new Vector2(0, 0);
        }
    }

    public void Damage(int damage, Vector3 attacker)
    {
        Instantiate(_hiteffect, transform.position, quaternion.identity);
        if (_stunned && damage == 2)
        {
            _dead = true;
            // KILL KILL KILL
        }
        else
        {
            Launch(attacker);
        }
    }

    void Launch(Vector3 attacker)
    {
        Stun();
        Vector2 _launchVelocity = new Vector2(0 , 0);
        if (attacker.x - this.transform.position.x < 0) 
            // negative means attacker is on the left (2 - 4 < 0)
        {
            _launchVelocity = new Vector2(_launchLength, _launchHeight);
        }
        else
        {
            _launchVelocity = new Vector2(_launchLength * -1, _launchHeight);
        }
        _body.velocity = new Vector2(0, 0);
        _body.velocity = _launchVelocity;
    }
    
    void Stun()
    {
        _stunned = true;
        _move.enabled = false;
        _jump.enabled = false;
        _attack.enabled = false;
    }

    void UnStun()
    {
        _stunned = false;
        _move.enabled = true;
        _jump.enabled = true;
        _attack.enabled = true;
    }

    public bool isStunned()
    {
        return _stunned;
    }

    public bool isDead()
    {
        return _dead;
    }
}   
    
