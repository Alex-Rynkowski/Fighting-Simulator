using System;
using System.ComponentModel;
using UnityEngine;

namespace Player_scripts.Jump_Mechanics
{
    public class SideJump : JumpMechanic
    {
        
        protected override float MouseAxisStartPosition => MouseStartPosition.x;

        protected override float MouseAxisCurrentPosition => MouseCurrentPosition.x;

        protected override float MouseRequiredMovement => (ScreenHeight / ScreenWidth) * (mouseRequiredMovement / 100);

        protected override void MousePosOnInput()
        {
            if (!Input.GetKeyDown(UserInput) || !PlayerServices.IsGrounded) return;
            MouseStartPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisStartPosition = MouseStartPosition.x;
            JumpTime = Time.time;
        }

        protected override void DisableJumpMechanic()
        {
            foreach (var jumpMechanic in JumpMechanics)
            {
                if (jumpMechanic == this) continue;
                jumpMechanic.enabled = false;
            }
        }

        void Update()
        {
            print(MouseRequiredMovement);
            MouseAxisCurrentPosition = MouseCurrentPosition.x;

            HorizontalJump(new Vector3(horizontalForce, verticalForce, 0));
            EnableJumpMechanic();
        }
    }
}