using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private GameObject shieldPrefab;
        [SerializeField] private int maxHealth = 1;
        [SerializeField] private int launchForce = 5000;
        [SerializeField] private GameObject shipModel;
        [SerializeField] private int powerUpState;
        
        private bool _hasDoubleShot;
        private bool _hasMissile;
        private bool _hasLaser;
        private bool _canSpawnShield;
        private bool _isAlive = true;
        
        public bool HasShield { get; set; }
        private int CurrentHealth { get; set; }
        
        private Rigidbody2D _rigidbody2D;
        private PolygonCollider2D _polygonCollider2D;
        
        private CameraController _cameraController; //refers to CameraController script
        private Camera _mainCamera; //refers to the actual Camera itself

        private void Start()
        {
            CurrentHealth = maxHealth;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _cameraController = FindObjectOfType<CameraController>();
            _mainCamera = Camera.main;
        }
    
        private void Update() 
        {
            CameraClamp();
            CameraFollow();
            ControlPlayer();
            HealthCheck();
            ShieldCheck();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                CurrentHealth--;
            }
        
            if (other.CompareTag("Obstacle"))
            {
                CurrentHealth = 0;
            }

            if (other.CompareTag("PowerUp"))
            {
                powerUpState++;
                Debug.Log(powerUpState);
                Destroy(other.gameObject);
            }
        }

        private void ControlPlayer() //updates player position based on input 
        {
            if (_isAlive)
            {
                float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
                float yMovement = Input.GetAxis("Vertical") * moveSpeed;
                transform.position += new Vector3(xMovement, yMovement, 0);
                RotateModel(yMovement);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                
                    Shoot(launchForce, 0, Quaternion.identity, _hasLaser);
                    if (_hasDoubleShot)
                    {
                        Shoot(launchForce, launchForce, Quaternion.AngleAxis(45, new Vector3(0, 0, 45)), false);
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    PowerUp();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }


        }

        private void Shoot(float xForce, float yForce, Quaternion angle, bool hasLaser)
        {
            GameObject shot;
            if (hasLaser)
            {
                shot = Instantiate(laserPrefab, _rigidbody2D.position + new Vector2(1.3f, 0), angle);
            }
            else
            {
                shot = Instantiate(projectilePrefab, _rigidbody2D.position + new Vector2(1.3f, 0), angle);
            }
            
            ProjectileController projectile = shot.GetComponent<ProjectileController>();
            projectile.Launch(xForce, yForce);
        }
    
        private void HealthCheck() //checks if the player should be dead or not
        {
            if (CurrentHealth == 0 && _isAlive)
            {
                MeshRenderer model = shipModel.GetComponent<MeshRenderer>();
                model.enabled = false;
                moveSpeed = 0;
                _isAlive = false;
            }
        }

        private void PowerUp() //activates when the player presses the powerup button; grants the player a powerup based on their powerup state
        {
            switch (powerUpState)
            {
                case 1:
                    moveSpeed *= 1.2f;
                    powerUpState = 0;
                    break;
                case 2:
                    if (!_hasMissile)
                    {
                        StartCoroutine(Missile());
                        powerUpState = 0; 
                    }
                    break;
                case 3:
                    _hasDoubleShot = true;
                    powerUpState = 0;
                    break;
                case 4:
                    _hasLaser = true;
                    powerUpState = 0;
                    break;
                case 5:
                    Debug.Log("OPTION");
                    powerUpState = 0;
                    break;
                case 6:
                    _canSpawnShield = true;
                    powerUpState = 0;
                    break;
                case 7:
                    powerUpState = 1;
                    break;
            }
        }

        private IEnumerator Missile() //shoots a missile downwards every second when activated
        {
            _hasMissile = true;
            while(true)
            {
                Shoot(launchForce/2.0f, -launchForce/2.0f, Quaternion.AngleAxis(-45, new Vector3(0, 0, 45)), false);
                yield return new WaitForSeconds(1);
            }
        }

        private void ShieldCheck() //spawns a shield if the player does not have one already, and requests one by pressing shift while their powerUpState is 6
        {
            if (_canSpawnShield && !HasShield)
            {
                Instantiate(shieldPrefab);
                HasShield = true;
                _canSpawnShield = false;
            }
        }

        private void RotateModel(float yMovement)
        {
            if (yMovement > 0)
            {
                shipModel.transform.eulerAngles = new Vector3(-491, -180, 90);
            }
            else if (yMovement < 0)
            {
                shipModel.transform.eulerAngles = new Vector3(-411, -180, 90);
            }
            else
            {
                shipModel.transform.eulerAngles = new Vector3(-451, -180, 90);
            }
        }
        
        private void CameraFollow() //updates player's x position to match camera's
        {
            transform.position += new Vector3(_cameraController.CamSpeed, 0, 0);
        }

        private void CameraClamp() //prevents player from leaving boundaries of camera
        {
            Vector3 pos = _mainCamera.WorldToViewportPoint (transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = _mainCamera.ViewportToWorldPoint(pos);
        }
    }
}
