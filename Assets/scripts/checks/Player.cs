using System.Collections;
using System.Collections.Generic;
using Assets.scripts.capabilities;
using Assets.scripts.checks;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Move _move;
    private Jump _jump;
    private Attack _attack;
    private Rigidbody2D _body;
    private Ground _ground;
    private bool _stunned = false;

    private bool _onGround;
    
    [SerializeField, Range(0f, 10f)] private float _launchHeight = 3f;
    [SerializeField, Range(0f, 10f)] private float _launchLength = 3f;
    
    
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
        _onGround = _ground.GetOnGround();
        
    }

    void FixedUpdate()
    { 
        if (_stunned && _onGround && _body.velocity.y <= 0)
        {
            _stunned = false;
            /*_move.enabled = true;
            _jump.enabled = true;
            _attack.enabled = true;*/
            _body.velocity = new Vector2(0, 0);
        }
    }

    public void Damage(int damage)
    {
        if (_stunned && damage == 2)
        {
            // KILL KILL KILL
        }
        else
        {
            Launch();
        }
    }

    void Launch()
    {
        _stunned = true;
        _move.enabled = false;
        _jump.enabled = false;
        _attack.enabled = false;
        
        Vector2 _launchVelocity = new Vector2(_launchLength, _launchHeight);
        _body.velocity = new Vector2(0, 0);
        _body.velocity = _launchVelocity;
    }

    public bool isStunned()
    {
        return _stunned;
    }
}
