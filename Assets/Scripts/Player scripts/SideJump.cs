using System;
using UnityEngine;

namespace Player_scripts
{
    public class SideJump : JumpMechanic
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        protected override float MouseAxisStartPosition => MouseStartPosition.x - .5f;

        protected override float MouseAxisCurrentPosition => MouseCurrentPosition.x -.5f;

        protected override void MousePosOnInput()
        {
            if (!Input.GetKeyDown(KeyCode.E) || !PlayerServices.IsGrounded) return;
            
            MouseStartPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisStartPosition = MouseStartPosition.x;
            JumpTime = Time.time;
        }

        void Update()
        {
            MouseCurrentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisCurrentPosition = MouseCurrentPosition.x;
            
            HorizontalJump(KeyCode.E, new Vector3(horizontalForce, verticalForce, 0));
        }
    }
}