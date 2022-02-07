using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int launchForce = 5000;
    [SerializeField] private GameObject shipModel;
    [SerializeField] private int powerUpState = 0;
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

        if (other.CompareTag("PowerUp"))
        {
            powerUpState++;
            Destroy(other.gameObject);
        }
    }

    private void ControlPlayer() //updates player position based on input 
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float yMovement = Input.GetAxis("Vertical") * moveSpeed;
        transform.position += new Vector3(xMovement, yMovement, 0);
        RotateModel(yMovement);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PowerUp();
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
        GameObject shot = Instantiate(projectilePrefab, _rigidbody2D.position + new Vector2(1.7f, -0.1f), Quaternion.identity);
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

    private void PowerUp()
    {
        switch (powerUpState)
        {
            case 1:
                Debug.Log("SPEEDUP");
                powerUpState = 0;
                break;
            case 2:
                Debug.Log("MISSILE");
                powerUpState = 0;
                break;
            case 3:
                Debug.Log("DOUBLE");
                powerUpState = 0;
                break;
            case 4:
                Debug.Log("LASER");
                powerUpState = 0;
                break;
            case 5:
                Debug.Log("OPTION");
                powerUpState = 0;
                break;
            case 6: 
                Debug.Log("?");
                powerUpState = 0;
                break;
            case 7:
                powerUpState = 1;
                break;
        }
    }

    private void RotateModel(float yMovement)
    {
        //bool isRotating = yMovement != 0;
        if (yMovement > 0)
        {
            shipModel.transform.eulerAngles = new Vector3(-491, -180, 90);
            //shipModel.transform.Rotate(new Vector3(0, -40, 0));
        }
        else if (yMovement < 0)
        {
            //shipModel.transform.Rotate(new Vector3(0, 40, 0));
            shipModel.transform.eulerAngles = new Vector3(-411, -180, 90);
        }
        else
        {
            //shipModel.transform.Rotate(new Vector3(0, 0, 0));
            shipModel.transform.eulerAngles = new Vector3(-451, -180, 90);
        }
    }
}
