using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speed;
    private CameraController _cameraController; //refers to CameraController script
    private Camera _mainCamera; //refers to the actual Camera itself
    
    private void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        _mainCamera = Camera.main;
    }
    
    private void Update() 
    {
        CameraClamp();
        CameraFollow();
        ControlPlayer();
    }
    
    private void ControlPlayer() //updates player position based on player input
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;
        float yMovement = Input.GetAxis("Vertical") * speed;
        transform.position += new Vector3(xMovement, yMovement, 0);
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
