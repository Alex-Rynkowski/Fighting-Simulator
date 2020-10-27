using System;
using UnityEngine;

namespace Player
{
    public class SideJump : MonoBehaviour
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        Rigidbody _rb;

        float _sideJumpTimer;
        Ray _mouseStartPosition;
        Ray _mouseCurrentPosition;
        bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground"));

        bool CanSideJump =>
            (Mathf.Abs(_mouseCurrentPosition.direction.x) - Mathf.Abs(_mouseStartPosition.direction.x) >= .5f ||
             Mathf.Abs(_mouseStartPosition.direction.x) - Mathf.Abs(_mouseCurrentPosition.direction.x) >= .5f) &&
            Time.time - _sideJumpTimer <= 1;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            _mouseCurrentPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            DoSideJump(KeyCode.E, this.horizontalForce, this.verticalForce);
        }

        void DoSideJump(KeyCode userInput, float horizontalForce, float verticalForce)
        {
            var whileLoopTime = 0f;

            if (!Input.GetKey(userInput) || !IsGrounded) return;
            if (Input.GetKeyDown(userInput) && IsGrounded)
            {
                _mouseStartPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
                _sideJumpTimer = Time.time;
            }

            if (!CanSideJump) return;

            var mpStartPos = _mouseStartPosition.direction.x;
            var mpCurrentPos = _mouseCurrentPosition.direction.x;

            _sideJumpTimer = 0;

            _rb.velocity = Vector3.zero;
            while (whileLoopTime < 1)
            {
                whileLoopTime += Time.deltaTime;

                _rb.AddForce(mpCurrentPos > mpStartPos
                    ? new Vector3(horizontalForce * Time.deltaTime, verticalForce * Time.deltaTime, 0)
                    : new Vector3(-horizontalForce * Time.deltaTime, verticalForce * Time.deltaTime, 0));
            }
        }
    }
}