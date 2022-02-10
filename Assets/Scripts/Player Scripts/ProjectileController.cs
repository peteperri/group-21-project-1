using UnityEngine;

namespace Player_Scripts
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        private PlayerController _player;
        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            _player = FindObjectOfType<PlayerController>();
        }
    
        private void Update() //kills projectiles when they get too far away
        {
            if (transform.position.x - _player.transform.position.x > 20.0f || !_player.IsAlive)
            {
                Destroy(gameObject);
            }
        }

        public void Launch(float xForce, float yForce) //shoot method
        {
            rigidbody2d.AddForce(new Vector2(xForce, yForce));
        }
    }
}