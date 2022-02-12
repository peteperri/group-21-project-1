using UnityEngine;

namespace Player_Scripts
{
    public class ParticleController : MonoBehaviour
    {
        private PlayerController _player;
        
        void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }
        
        
        void Update()
        {
            transform.position = _player.transform.position - new Vector3(0.94f, 0.03f, 0);
            if (!_player.IsAlive)
            {
                Destroy(gameObject);
            }
        }
    }
}

