using System.Collections;
using System.Collections.Generic;
using Assets.scripts.checks;
using Assets.scripts.controllers;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    private AnimationController _animationController;
    private bool _onGround;

    [SerializeField]private float _attackSpeed;
    private float _attackTimer;
    // private int _activeFrames
    // private int _frameDelay;     for animation startup to avoid every move being frame 1
    // (because not every animation hits frame 1) (though idk if thats an issue) (yet)
    
    [SerializeField]public GameObject katana_attack1;
    
    private bool _desiredAttack;
    

    public LayerMask EnemyMask;

    [SerializeField] public Transform attackPos;
    [SerializeField] public Vector2 hitboxSize;

    // Start is called before the first frame update
    void Start()
    {
        _animationController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        _desiredAttack |= _controller.RetrieveAttackInput();
        if (_attackTimer >= 0)
        {
            _attackTimer -= Time.deltaTime; 
        }
    }

    void FixedUpdate()
    {
        if (_desiredAttack && _attackTimer <= 0)
        {
            _desiredAttack = false;
            AttackAction();
            _attackTimer = _attackSpeed;
        }
    }

    void AttackAction()
    {
        GameObject swing = Instantiate(katana_attack1, attackPos.position, Quaternion.identity);
        if (!_animationController.isFacingRight())
        {
            swing.transform.localScale = new Vector3(-1, 1, 1);
        }

        swing.transform.parent = this.gameObject.transform;
        
        Collider2D[] playersToDamage = Physics2D.OverlapBoxAll(attackPos.position, hitboxSize, 0f, EnemyMask);
        for (int i = 0; i < playersToDamage.Length; i++)
        {
            if (playersToDamage[i].gameObject.name != this.gameObject.name)
            {
                Debug.Log(playersToDamage[i].gameObject.name);
                playersToDamage[i].gameObject.GetComponent<Player>().Damage(1, this.transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(hitboxSize.x, hitboxSize.y, 1));
    }
}
