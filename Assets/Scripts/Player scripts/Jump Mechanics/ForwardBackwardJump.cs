using UnityEngine;

namespace Player_scripts.Jump_Mechanics
{
    public class ForwardBackwardJump : JumpMechanic
    {
        protected override float MouseAxisStartPosition => MouseStartPosition[0].y;

        protected override float MouseAxisCurrentPosition => MouseCurrentPosition[0].y;
        protected override float MouseRequiredMovement => (ScreenWidth / ScreenHeight) * (mouseRequiredMovement / 100);

        protected override void MousePosOnInput()
        {
            if (!Input.GetKeyDown(UserInput) || !PlayerServices.IsGrounded) return;
            MouseStartPosition.Clear();
            MouseStartPosition.Add(Camera.main.ScreenToViewportPoint(Input.mousePosition));
            MouseAxisStartPosition = MouseStartPosition[0].y;
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
            MouseAxisCurrentPosition = MouseCurrentPosition[0].y;

            HorizontalJump(new Vector3(0, verticalForce, horizontalForce));
            EnableJumpMechanic();
        }
    }
}