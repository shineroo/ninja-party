using System.Collections;
using System.Collections.Generic;
using Assets.scripts.checks;
using Assets.scripts.controllers;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    private Ground _ground;
    private bool _onGround;
    
    private float _attackTimer;
    private float _attackSpeed;
    private bool _desiredAttack;
    
    public LayerMask EnemyMask;

    [SerializeField] public Transform attackPos;
    [SerializeField] public Vector2 hitboxSize;

    public int damage = 2;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        _desiredAttack |= _controller.RetrieveAttackInput();
    }

    void FixedUpdate()
    {
        if (_desiredAttack)
        {
            _desiredAttack = false;
            AttackAction();
        }
    }

    void AttackAction()
    {
        Collider2D[] playersToDamage = Physics2D.OverlapBoxAll(attackPos.position, hitboxSize, 0f, EnemyMask);
        for (int i = 0; i < playersToDamage.Length; i++)
        {
            if (playersToDamage[i].gameObject.name != this.gameObject.name)
            {
                Debug.Log(playersToDamage[i].gameObject.name);
                // playersToDamage[i].GetComponent<Player>.Damage(damage);
                // it should launch the player
                // and if the player is launched and damage is 1 it should relaunch them(?)
                // and if the player is launched and damage is 2 it should kill them
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(hitboxSize.x, hitboxSize.y, 1));
    }
}
