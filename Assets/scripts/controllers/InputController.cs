using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput();
    
        public abstract bool RetrieveJumpInput();

        public abstract bool RetrieveJumpRelease();
    }
    
}

