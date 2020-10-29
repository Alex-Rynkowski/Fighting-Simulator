using System.Collections.Generic;
using UnityEngine;

namespace Player_scripts.Jump_Mechanics
{
    public abstract class JumpMechanic : MonoBehaviour
    {
        [Tooltip("How much mouse movement is required before character jumps? Between 0-100")] [SerializeField]
        protected float mouseRequiredMovement;

        [Header("Jump Force")] [SerializeField]
        protected float horizontalForce;

        [SerializeField] protected float verticalForce;
        protected readonly float ScreenWidth = Screen.width;
        protected readonly float ScreenHeight = Screen.height;

        protected const KeyCode UserInput = KeyCode.E;
        protected List<Vector3> MouseStartPosition = new List<Vector3>();
        protected List<Vector3> MouseCurrentPosition = new List<Vector3>();
        protected static JumpMechanic[] JumpMechanics;

        protected virtual float MouseRequiredMovement { get; }
        protected float JumpTime { get; set; }
        protected virtual float MouseAxisStartPosition { get; set; }
        protected virtual float MouseAxisCurrentPosition { get; set; }

        void Start()
        {
            JumpMechanics = FindObjectsOfType<JumpMechanic>();

            //2 positions is required for the diagonal jumps (x, y)
            MouseStartPosition.Add(Camera.main.ScreenToViewportPoint(Input.mousePosition));
            MouseStartPosition.Add(Camera.main.ScreenToViewportPoint(Input.mousePosition));
            MouseCurrentPosition.Add(Camera.main.ScreenToViewportPoint(Input.mousePosition));
            MouseCurrentPosition.Add(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }

        protected abstract void MousePosOnInput();
        protected abstract void DisableJumpMechanic();

        protected static void EnableJumpMechanic()
        {
            if (!Input.GetKeyUp(UserInput)) return;
            foreach (var jumpMechanic in JumpMechanics)
            {
                jumpMechanic.enabled = true;
            }
        }

        bool CanJump =>
            (Mathf.Abs(MouseAxisCurrentPosition) - Mathf.Abs(MouseAxisStartPosition) >= MouseRequiredMovement ||
             Mathf.Abs(MouseAxisStartPosition) - Mathf.Abs(MouseAxisCurrentPosition) >= MouseRequiredMovement) &&
            Time.time - JumpTime <= 1;

        protected void HorizontalJump(Vector3 direction)
        {
            MouseCurrentPosition[0] = Camera.main.ScreenToViewportPoint(Input.mousePosition);
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
                    : new Vector3(-direction.x, direction.y, -direction.z) * Time.deltaTime);
            }
        }
    }
}