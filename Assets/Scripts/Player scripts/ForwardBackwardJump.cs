using UnityEngine;

namespace Player_scripts
{
    public class ForwardBackwardJump : JumpMechanic
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        protected override float MouseAxisStartPosition => MouseStartPosition.y -.5f; //"-.5f" represents the pivot

        protected override float MouseAxisCurrentPosition => MouseCurrentPosition.y - .5f;

        protected override void MousePosOnInput()
        {
            if (!Input.GetKeyDown(KeyCode.E) || !PlayerServices.IsGrounded) return;
            MouseStartPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisStartPosition = MouseStartPosition.y; 
            JumpTime = Time.time;
        }

        void Update()
        {
            MouseCurrentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            MouseAxisCurrentPosition = MouseCurrentPosition.y;
            
            HorizontalJump(KeyCode.E, new Vector3(0, verticalForce, horizontalForce));
        }
    }
}