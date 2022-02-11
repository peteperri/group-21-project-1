using System.Collections;
using Player_Scripts;
using UnityEngine;

namespace Enemy_Scripts.Turret
{
    public class TurretController : EnemyController
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private bool upsideDown;
        private Rigidbody2D _rigidbody2D;
        private float _xDirection;
        private float _yDirection;
        private new void Start()
        {
            Player = FindObjectOfType<PlayerController>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            StartCoroutine(ShootAtPlayer());

            if (upsideDown)
            {
                _yDirection = -1;
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1)) ;
            }
            else
            {
                _yDirection = 1;
            }
        }

        private new void Update()
        {
            DistanceCheck();
            LookAtPlayer();
            
        }
        private void LookAtPlayer()
        {
            if (Player.transform.position.x < transform.position.x)
            {
                _xDirection = -1;
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                _xDirection = 1;
                transform.eulerAngles = new Vector3(0, -180, 90);
            }
        }

        private IEnumerator ShootAtPlayer()
        {
            while (Player.IsAlive)
            {   GameObject shot = Instantiate(bullet, _rigidbody2D.position + new Vector2(_xDirection, _yDirection), Quaternion.identity);
                ProjectileController projectile = shot.GetComponent<ProjectileController>();
                projectile.Launch(500 * _xDirection, 1000 * _yDirection);
                yield return new WaitForSeconds(2);
            }
        }
    }
}