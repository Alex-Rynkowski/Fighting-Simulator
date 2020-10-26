using System;
using UnityEngine;

namespace Player
{
    public class SideWayJump : MonoBehaviour
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        [SerializeField] float maxVelocity;
        [SerializeField] float maxForce;
        Rigidbody _rb;

        float _whileTimer = 0;

        bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground"));

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            // var mp = Camera.main.ScreenPointToRay(Input.mousePosition);
            // print(mp);
            SideJump(KeyCode.E, this.horizontalForce, this.verticalForce);
            SideJump(KeyCode.Q, -this.horizontalForce, this.verticalForce);
            
        }

        void SideJump(KeyCode userInput, float horizontalForce, float verticalForce)
        {
            if (Input.GetKeyDown(userInput) && IsGrounded)
            {
                _rb.velocity = Vector3.zero;
                _whileTimer = 0;
                while (_whileTimer < 1)
                {
                    _whileTimer += Time.deltaTime;
                    _rb.AddForce(new Vector3(horizontalForce, verticalForce, 0));
                }
            }
        
        }
    }
}