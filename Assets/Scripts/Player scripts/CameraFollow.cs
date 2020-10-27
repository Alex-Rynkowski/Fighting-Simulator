using UnityEngine;

namespace Player_scripts
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
