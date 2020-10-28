using UnityEngine;

namespace Player_scripts
{
    public abstract class JumpMechanic : MonoBehaviour
    {
        protected Vector3 MouseStartPosition;
        protected Vector3 MouseCurrentPosition;
        protected float JumpTime;

        protected virtual float MouseAxisStartPosition { get; set; }
        protected virtual float MouseAxisCurrentPosition { get; set; }

        protected abstract void MousePosOnInput();

        bool CanJump =>
            (Mathf.Abs(MouseAxisCurrentPosition) - Mathf.Abs(MouseAxisStartPosition) >= .2f ||
             Mathf.Abs(MouseAxisStartPosition) - Mathf.Abs(MouseAxisCurrentPosition) >= .2f) &&
            Time.time - JumpTime <= 1;

        protected void HorizontalJump(KeyCode userInput, Vector3 direction)
        {
            if (!Input.GetKey(userInput) || !PlayerServices.IsGrounded) return;

            MousePosOnInput();

            if (!CanJump) return;
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
            JumpTime = 0;
        }
    }
}