using UnityEngine;

namespace Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] GameObject player;
        
        void Update()
        {
            transform.position = player.transform.position;

        }
    }
}
