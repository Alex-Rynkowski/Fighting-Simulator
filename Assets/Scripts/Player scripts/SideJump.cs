using System;
using UnityEngine;

namespace Player_scripts
{
    public class SideJump : JumpMechanic
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        protected override float MouseAxisStartPosition => MouseStartPosition.x;

        protected override float MouseAxisCurrentPosition => MouseCurrentPosition.x;

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
            MouseCurrentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisCurrentPosition = MouseCurrentPosition.x;

            HorizontalJump(new Vector3(horizontalForce, verticalForce, 0));
            if (Input.GetKeyUp(UserInput)) EnableJumpMechanic();
        }
    }
}