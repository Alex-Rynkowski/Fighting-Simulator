using UnityEngine;

namespace Player_scripts
{
    public class ForwardBackwardJump : MonoBehaviour
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;

        float _FwBwJumpTime;
        Ray _mouseStartPosition;
        Ray _mouseCurrentPosition;


        bool CanFwBwJump =>
            (Mathf.Abs(_mouseCurrentPosition.direction.y) - Mathf.Abs(_mouseStartPosition.direction.y) >= .5f ||
             Mathf.Abs(_mouseStartPosition.direction.y) - Mathf.Abs(_mouseCurrentPosition.direction.y) >= .5f) &&
            Time.time - _FwBwJumpTime <= 1;

        void Update()
        {
            _mouseCurrentPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            DoFwBwJimp(KeyCode.E, this.horizontalForce, this.verticalForce);
        }

        void DoFwBwJimp(KeyCode userInput, float horizontalForce, float verticalForce)
        {
            if (!Input.GetKey(userInput) || !PlayerServices.IsGrounded) return;
            if (Input.GetKeyDown(userInput) && PlayerServices.IsGrounded)
            {
                _mouseStartPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
                _FwBwJumpTime = Time.time;
            }

            if (!CanFwBwJump) return;
            var mouseStartPos = _mouseStartPosition.direction.z;
            var mouseCurrentPos = _mouseCurrentPosition.direction.z;

            _FwBwJumpTime = 0;
            var whileLoopTime = 0f;

            PlayerServices.Rb.velocity = Vector3.zero;
            while (whileLoopTime < 1)
            {
                whileLoopTime += Time.deltaTime;

                PlayerServices.Rb.AddForce(mouseCurrentPos > mouseStartPos
                    ? new Vector3(0, verticalForce * Time.deltaTime, horizontalForce * Time.deltaTime)
                    : new Vector3(0, verticalForce * Time.deltaTime, -horizontalForce * Time.deltaTime));
            }
        }
    }
}