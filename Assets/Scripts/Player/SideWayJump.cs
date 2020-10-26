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
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            print(_rb.velocity);
            // var mp = Camera.main.ScreenPointToRay(Input.mousePosition);
            // print(mp);
            if (Input.GetKeyDown(KeyCode.E))
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