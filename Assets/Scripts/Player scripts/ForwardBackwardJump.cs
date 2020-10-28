using System;
using UnityEngine;

namespace Player_scripts
{
    public class ForwardBackwardJump : JumpMechanic
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        protected override float MouseAxisStartPosition => MouseStartPosition.y;

        protected override float MouseAxisCurrentPosition => MouseCurrentPosition.y;

        protected override void MousePosOnInput()
        {
            if (!Input.GetKeyDown(UserInput) || !PlayerServices.IsGrounded) return;
            MouseStartPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisStartPosition = MouseStartPosition.y; 
            JumpTime = Time.time;
        }
        
        protected override void DisableJumpMechanic()
        {
            foreach (var jumpMechanic in JumpMechanics)
            {
                if(jumpMechanic == this) continue;
                jumpMechanic.enabled = false;
            }
        }

        void Update()
        {
            MouseAxisCurrentPosition = MouseCurrentPosition.y;
            
            HorizontalJump(new Vector3(0, verticalForce, horizontalForce));
            if (Input.GetKeyUp(UserInput)) EnableJumpMechanic();
        }
    }
}