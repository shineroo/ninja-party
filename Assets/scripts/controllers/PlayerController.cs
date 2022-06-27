using UnityEngine;

namespace Assets.scripts.controllers
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public override bool RetrieveJumpInput()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}