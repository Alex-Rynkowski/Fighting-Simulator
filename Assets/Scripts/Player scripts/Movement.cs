using UnityEngine;

namespace Player_scripts
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float moveForce = 10;

        void Update()
        {
            ForwardBackwardMovement(KeyCode.W, moveForce);
            ForwardBackwardMovement(KeyCode.S, -moveForce);
            LeftRightMovement(KeyCode.D, moveForce);
            LeftRightMovement(KeyCode.A, -moveForce);
        }

        void ForwardBackwardMovement(KeyCode userInput, float moveForce)
        {
            if (!Input.GetKey(userInput) && PlayerServices.IsGrounded) return;

            if (Mathf.Abs(PlayerServices.Rb.velocity.z) < 10)
            {
                PlayerServices.Rb.AddForce(Vector3.forward * moveForce);
                return;
            }

            PlayerServices.Rb.velocity = Vector3.forward * moveForce;
        }

        void LeftRightMovement(KeyCode userInput, float moveForce)
        {
            if (Input.GetKey(userInput) && PlayerServices.IsGrounded)
            {
                PlayerServices.Rb.AddForce(Vector3.right * moveForce);
            }
        }
    }
}