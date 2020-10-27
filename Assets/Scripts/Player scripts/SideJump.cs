using System;
using UnityEngine;

namespace Player_scripts
{
    public class SideJump : MonoBehaviour
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;

        float _sideJumpTime;
        Ray _mouseStartPosition;
        Ray _mouseCurrentPosition;


        bool CanSideJump =>
            (Mathf.Abs(_mouseCurrentPosition.direction.x) - Mathf.Abs(_mouseStartPosition.direction.x) >= .5f ||
             Mathf.Abs(_mouseStartPosition.direction.x) - Mathf.Abs(_mouseCurrentPosition.direction.x) >= .5f) &&
            Time.time - _sideJumpTime <= 1;

        void Update()
        {
            _mouseCurrentPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            DoSideJump(KeyCode.E, this.horizontalForce, this.verticalForce);
        }

        void DoSideJump(KeyCode userInput, float horizontalForce, float verticalForce)
        {
            if (!Input.GetKey(userInput) || !PlayerServices.IsGrounded) return;
            if (Input.GetKeyDown(userInput) && PlayerServices.IsGrounded)
            {
                _mouseStartPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
                _sideJumpTime = Time.time;
            }

            if (!CanSideJump) return;

            var mouseStartPos = _mouseStartPosition.direction.x;
            var mouseCurrentPos = _mouseCurrentPosition.direction.x;

            _sideJumpTime = 0;
            var whileLoopTime = 0f;
            
            PlayerServices.Rb.velocity = Vector3.zero;
            while (whileLoopTime < 1)
            {
                whileLoopTime += Time.deltaTime;

                PlayerServices.Rb.AddForce(mouseCurrentPos > mouseStartPos
                    ? new Vector3(horizontalForce * Time.deltaTime, verticalForce * Time.deltaTime, 0)
                    : new Vector3(-horizontalForce * Time.deltaTime, verticalForce * Time.deltaTime, 0));
            }
        }
    }
}