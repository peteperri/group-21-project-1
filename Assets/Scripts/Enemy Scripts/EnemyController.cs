using Player_Scripts;
using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int health = 1;
        private PlayerController _player;
    
        protected void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        protected void Update()
        {
            DistanceCheck();
        }

        protected void DistanceCheck()
        {
            if (_player.transform.position.x - transform.position.x  > 20.0f)
            {
                Destroy(gameObject);
            }
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            CheckIfDamaged(other);
        }
    
        private void CheckIfDamaged(Collider2D other)
        {
            if (other.CompareTag("Shot"))
            {
                health--;
                Destroy(other.gameObject);
                if (health == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

