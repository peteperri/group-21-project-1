using UnityEngine;

namespace Player_Scripts
{
    public class ShieldController : MonoBehaviour
    {
        private PlayerController _player;
        private Vector3 _offset;
        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            _offset = new Vector3(1.6f, 0.1f, 0);
        }
        
        private void Update()
        {
            transform.position = _player.transform.position + _offset;
            if (!_player.IsAlive)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
            {
                _player.HasShield = false;
                Destroy(gameObject);
            }
        }
    }
}

