using Assets.scripts.checks;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.capabilities
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private PlayerController _controller;
        private Rigidbody2D _body;
        private Ground _ground;
        private bool _onGround;
        
        [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
        private bool _desiredJump;
        [SerializeField]private bool _jumpRelease;
        private int _jumpPhase;
        
        private Vector2 _velocity;

        // Start is called before the first frame update
        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
        }

        // Update is called once per frame
        void Update()
        {
            _desiredJump |= _controller.RetrieveJumpInput();
            _jumpRelease |= _controller.RetrieveJumpRelease();
        }

        private void FixedUpdate()
        {
            _onGround = _ground.GetOnGround();
            _velocity = _body.velocity;

            if (_onGround)
            {
                _jumpPhase = 0;
            }

            if (_desiredJump)
            {
                _desiredJump = false;
                JumpAction();
            }

            if (_body.velocity.y > 0 && _jumpRelease)
            {
                _velocity.y = 0;
            }
            _jumpRelease = false;
            _body.velocity = _velocity;
        }
        private void JumpAction()
        {
            if (_onGround || _jumpPhase < _maxAirJumps)
            {
                _jumpPhase += 1;

                _velocity = new Vector2(_body.velocity.x, _jumpHeight);
            }
        }
    }
}
