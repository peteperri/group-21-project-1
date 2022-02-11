using System.Collections;
using Camera_Scripts;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy_Scripts.Boss
{
    public class BossController : EnemyController
    {
        private Camera _camera;
        private CameraController _cameraController;
        private Rigidbody2D _rigidbody2D;
        private bool _startedShooting;
        [SerializeField] private GameObject bullet;


        private new void Start()
        {
            base.Start();
            _camera = Camera.main;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _cameraController = FindObjectOfType<CameraController>();
        }

        private new void Update()
        {
            
            Vector3 relativePosition = _camera.transform.InverseTransformDirection(transform.position - _camera.transform.position);
            float y = Mathf.PingPong(Time.time, 3) * 3 - 4;
            transform.position = new Vector3(transform.position.x, y, transform.position.z) * Time.deltaTime;
            
            if (relativePosition.x < 6.0f)
            {
                CameraClamp();
                Debug.Log(transform.position.y);
                CameraFollow();
                if (!_startedShooting)
                {
                    StartCoroutine(ShootAtPlayer());
                    _startedShooting = true;
                }
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        private void CameraFollow() 
        {
            transform.position += new Vector3(_cameraController.CamSpeed, 0, 0) * Time.deltaTime;
        }
        
        private void CameraClamp() 
        {
            Vector3 pos = _camera.WorldToViewportPoint (transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = _camera.ViewportToWorldPoint(pos);
        }
        
        
        private IEnumerator ShootAtPlayer()
        {
            while (Player.IsAlive)
            {
                for (int i = -2; i <= 2; i += 4)
                {
                    GameObject shot = Instantiate(bullet, _rigidbody2D.position + new Vector2(-0.5f ,i), Quaternion.identity);
                    ProjectileController projectile = shot.GetComponent<ProjectileController>();
                    projectile.Launch(-500, 0);
                }
                yield return new WaitForSeconds(2);
                //yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
            }
        }
    }
}


