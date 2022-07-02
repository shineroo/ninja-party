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
    // private int _frameDelay;     for animation startup to avoid every move being frame 1
    // (because not every animation hits frame 1) (though idk if thats an issue) (yet)
    
    public LayerMask EnemyMask;

    [SerializeField] public Transform attackPos;
    [SerializeField] public Vector2 hitboxSize;

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
                playersToDamage[i].gameObject.GetComponent<Player>().Damage(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(hitboxSize.x, hitboxSize.y, 1));
    }
}
