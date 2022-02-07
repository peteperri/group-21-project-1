using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int launchForce = 5000;
    private int CurrentHealth { get; set; }
    private Rigidbody2D _rigidbody2D;
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
    }

    private void ControlPlayer() //updates player position based on input 
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float yMovement = Input.GetAxis("Vertical") * moveSpeed;
        transform.position += new Vector3(xMovement, yMovement, 0);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
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

    private void Shoot() 
    {
        GameObject shot = Instantiate(projectilePrefab, _rigidbody2D.position + new Vector2(0.5f, 0), Quaternion.identity);
        ProjectileController projectile = shot.GetComponent<ProjectileController>();
        projectile.Launch(launchForce);
    }
    
    private void HealthCheck()
    {
        if (CurrentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
