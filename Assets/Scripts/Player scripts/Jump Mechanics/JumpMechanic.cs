using UnityEngine;

namespace Player_scripts.Jump_Mechanics
{
    public abstract class JumpMechanic : MonoBehaviour
    {
        [SerializeField] protected float mouseRequiredMovement;
        [SerializeField] protected float horizontalForce;
        [SerializeField] protected float verticalForce;
        protected float ScreenWidth = Screen.width;
        protected float ScreenHeight = Screen.height;
        
        protected const KeyCode UserInput = KeyCode.E;
        protected Vector3 MouseStartPosition;
        protected Vector3 MouseCurrentPosition;
        protected static JumpMechanic[] JumpMechanics;
        
        protected virtual float MouseRequiredMovement { get;}
        protected float JumpTime { get; set; }
        protected virtual float MouseAxisStartPosition { get; set; }
        protected virtual float MouseAxisCurrentPosition { get; set; }

        void Start()
        {
            JumpMechanics = FindObjectsOfType<JumpMechanic>();  
        }

        protected abstract void MousePosOnInput();
        protected abstract void DisableJumpMechanic();
        protected static void EnableJumpMechanic()
        {
            if(!Input.GetKeyUp(UserInput)) return;
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
                    : new Vector3(-direction.x, direction.y, -direction.z) * Time.deltaTime);
            }
        }


    }
}