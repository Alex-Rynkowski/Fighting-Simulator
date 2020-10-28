using UnityEngine;

namespace Player_scripts
{
    public abstract class JumpMechanic : MonoBehaviour
    {
        protected const KeyCode UserInput = KeyCode.E;
        protected Vector3 MouseStartPosition;
        protected Vector3 MouseCurrentPosition;
        protected static JumpMechanic[] JumpMechanics;

        protected float JumpTime { get; set; }
        protected virtual float MouseAxisStartPosition { get; set; }
        protected virtual float MouseAxisCurrentPosition { get; set; }

        void Start()
        {
            JumpMechanics = FindObjectsOfType<JumpMechanic>();  
        }

        protected abstract void MousePosOnInput();
        protected abstract void DisableJumpMechanic();

        bool CanJump =>
            (Mathf.Abs(MouseAxisCurrentPosition) - Mathf.Abs(MouseAxisStartPosition) >= .2f ||
             Mathf.Abs(MouseAxisStartPosition) - Mathf.Abs(MouseAxisCurrentPosition) >= .2f) &&
            Time.time - JumpTime <= 1;

        protected void HorizontalJump(Vector3 direction)
        {
            MouseCurrentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (!Input.GetKey(UserInput) || !PlayerServices.IsGrounded) return;

            MousePosOnInput();

            if (!CanJump) return;
            JumpTime = 0;

            DisableJumpMechanic();
            var mouseStartPos = MouseAxisStartPosition;
            var mouseCurrentPos = MouseAxisCurrentPosition;

            var whileLoopTime = 0f;

            PlayerServices.Rb.velocity = Vector3.zero;
            while (whileLoopTime < 1)
            {
                whileLoopTime += Time.deltaTime;

                PlayerServices.Rb.AddForce(mouseCurrentPos > mouseStartPos
                    ? direction * Time.deltaTime
                    : -direction * Time.deltaTime);
            }
        }

        protected static void EnableJumpMechanic()
        {
            foreach (var jumpMechanic in JumpMechanics)
            {
                jumpMechanic.enabled = true;
            }
        }
    }
}