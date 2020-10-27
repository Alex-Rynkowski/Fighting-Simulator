using UnityEngine;

namespace Player_scripts
{
    public static class PlayerServices
    {
        public static Rigidbody Rb = Object.FindObjectOfType<Player>().GetComponent<Rigidbody>();
        public static bool IsGrounded => Physics.Raycast(Object.FindObjectOfType<Player>().transform.position,
            Vector3.down, 1.1f, LayerMask.GetMask("Ground"));
    }
}