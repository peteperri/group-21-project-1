using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Camera_Scripts;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 1;
        [SerializeField] private int launchForce = 5000;
        [SerializeField] private float moveSpeed;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private GameObject shieldPrefab;
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private GameObject shipModel;
        [SerializeField] private Text powerUpInfo;
        
        private bool _hasDoubleShot;
        private bool _hasMissile;
        private bool _hasLaser;
        private bool _canSpawnShield;
        private string _powerUpString;
        private int _currentHealth;
        
        public bool HasShield { get; set; }
        public bool IsAlive { get; private set; } = true;
        public int PowerUpState { get; private set; }

        private Rigidbody2D _rigidbody2D;
        private CameraController _cameraController; //refers to CameraController script
        private Camera _mainCamera; //refers to the actual Camera itself

        private void Start()
        {
            _currentHealth = maxHealth;
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
            UpdatePowerUpString();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") || other.CompareTag("Obstacle"))
            {
                _currentHealth--;
            }
            
            if (other.CompareTag("PowerUp"))
            {
                PowerUpState++;
                if (PowerUpState >= 6)
                {
                    PowerUpState = 1;
                }
                Destroy(other.gameObject);
            }
        }

        private void ControlPlayer() //updates player position based on input 
        {
            if (IsAlive)
            {
                float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
                float yMovement = Input.GetAxis("Vertical") * moveSpeed;
                transform.position += new Vector3(xMovement, yMovement, 0);
                RotateModel(yMovement);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot(launchForce, 0, Quaternion.identity, _hasLaser, false);
                    if (_hasDoubleShot)
                    {
                        Shoot(launchForce, launchForce, Quaternion.AngleAxis(45, new Vector3(0, 0, 45)), false, false);
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    PowerUp();
                }
            }
            else
            {
                SceneManager.LoadScene(4);
            }
        }

        private void Shoot(float xForce, float yForce, Quaternion angle, bool hasLaser, bool isMissile)
        {
            float ySpawn = 0;
            GameObject prefab = projectilePrefab;
            if (hasLaser)
            {
                prefab = laserPrefab;
            } 
            else if (isMissile)
            {
                prefab = missilePrefab;
                ySpawn = -0.5f;
            }

            GameObject shot = Instantiate(prefab, _rigidbody2D.position + new Vector2(1.3f, ySpawn), angle);
            ProjectileController projectile = shot.GetComponent<ProjectileController>();
            projectile.Launch(xForce, yForce);
        }
    
        private void HealthCheck() //checks if the player should be dead or not
        {
            if (_currentHealth == 0 && IsAlive)
            {
                MeshRenderer model = shipModel.GetComponent<MeshRenderer>();
                model.enabled = false;
                moveSpeed = 0;
                IsAlive = false;
            }
        }

        private void PowerUp() //activates when the player presses the powerup button; grants the player a powerup based on their powerup state
        {
            _powerUpString = "NOTHING";
            switch (PowerUpState)
            {
                case 1:
                    moveSpeed *= 1.2f;
                    _cameraController.CamSpeed *= 1.2f;
                    PowerUpState = 0;
                    break;
                case 2:
                    if (!_hasMissile)
                    {
                        StartCoroutine(Missile());
                        PowerUpState = 0;
                    }
                    break;
                case 3:
                    if (!_hasDoubleShot)
                    {
                        _hasDoubleShot = true;
                        PowerUpState = 0;
                    }
                    break;
                case 4:
                    if (!_hasLaser)
                    {
                        _hasLaser = true;
                        PowerUpState = 0;
                    }
                    break;
                case 5:
                    if (!HasShield)
                    {
                        _canSpawnShield = true;
                        PowerUpState = 0; 
                    }
                    break;
            }
        }

        private void UpdatePowerUpString()
        {
            switch (PowerUpState)
            {
                case 0:
                    _powerUpString = "NOTHING";
                    break;
                case 1:
                    _powerUpString = "SPEEDUP";
                    break;
                case 2:
                    if (!_hasMissile)
                    {
                        _powerUpString = "MISSILE";
                    }
                    else
                    {
                        _powerUpString = "NOTHING \n(YOU ALREADY HAVE MISSILE)";
                    }
                    break;
                case 3:
                    if (!_hasDoubleShot)
                    {
                        _powerUpString = "DOUBLESHOT";
                    }
                    else
                    {
                        _powerUpString = "NOTHING \n(YOU ALREADY HAVE DOUBLESHOT)";
                    }
                    break;
                case 4:
                    if (!_hasLaser)
                    {
                        _powerUpString = "LASER";
                    }
                    else
                    {
                        _powerUpString = "NOTHING \n(YOU ALREADY HAVE LASER)";
                    }
                    break;
                case 5:
                    if (!_canSpawnShield && !HasShield)
                    {
                        _powerUpString = "SHIELD";
                    }
                    else
                    {
                        _powerUpString = "NOTHING \n(YOU ALREADY HAVE SHIELD)";
                    }
                    break;
            }
            
            powerUpInfo.text = $"PowerUp State: {PowerUpState}\nPress Shift for {_powerUpString}";
        }

        private IEnumerator Missile() //shoots a missile downwards every second when activated
        {
            _hasMissile = true;
            while(IsAlive)
            {
                Shoot(launchForce/2.0f, -launchForce/2.0f, Quaternion.AngleAxis(-45, new Vector3(0, 0, 1)), false, true);
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
