using System;
using UnityEngine;

namespace Player_scripts
{
    public class VerticalJump : MonoBehaviour
    {
        [SerializeField] float jumpForce;

        void Update()
        {
            Jump(KeyCode.Space, jumpForce);
        }

        void Jump(KeyCode userInput, float jumpForce)
        {
            if (!Input.GetKeyDown(userInput) || !PlayerServices.IsGrounded) return;

            float whileLoopTimer = 0;
            while (whileLoopTimer < 1)
            {
                whileLoopTimer += Time.deltaTime;

                PlayerServices.Rb.AddForce(Vector3.up * jumpForce * Time.deltaTime);
            }
        }
    }
}