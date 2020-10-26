using System;
using UnityEngine;

namespace Player
{
    public class SideWayJump : MonoBehaviour
    {
        [SerializeField] float horizontalForce;
        [SerializeField] float verticalForce;
        [SerializeField] float maxVelocity;
        Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            print(_rb.velocity);
            // var mp = Camera.main.ScreenPointToRay(Input.mousePosition);
            // print(mp);
            if (Input.GetKey(KeyCode.E))
            {
                _rb.AddForce(new Vector3(horizontalForce, verticalForce, 0));
            }
        }
    }
}