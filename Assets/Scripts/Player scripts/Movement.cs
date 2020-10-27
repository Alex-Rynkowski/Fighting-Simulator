﻿using UnityEngine;

namespace Player_scripts
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float moveForce = 10;

        Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            ForwardBackwardMovement(KeyCode.W, moveForce);
            ForwardBackwardMovement(KeyCode.S, -moveForce);
            LeftRightMovement(KeyCode.D, moveForce);
            LeftRightMovement(KeyCode.A, -moveForce);
        }

        void ForwardBackwardMovement(KeyCode userInput, float moveForce)
        {
            if (!Input.GetKey(userInput)) return;

            if (Mathf.Abs(_rb.velocity.z) < 10)
            {
                _rb.AddForce(Vector3.forward * moveForce);
                return;
            }

            _rb.velocity = Vector3.forward * moveForce;
        }

        void LeftRightMovement(KeyCode userInput, float moveForce)
        {
            if (Input.GetKey(userInput))
            {
                _rb.AddForce(Vector3.right * moveForce);
            }
        }
    }
}