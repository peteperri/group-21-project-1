using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float camSpeed;
    public float CamSpeed {get => camSpeed;} //getter property to access camSpeed in PlayerController

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera() //moves the camera across the game world at a rate of camSpeed per frame
    {
        transform.position += new Vector3(camSpeed, 0, 0);
    }
}
