using UnityEngine;

namespace Camera_Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float camSpeed;
        public float CamSpeed { get => camSpeed; set => camSpeed = value; } 

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
            transform.position += new Vector3(CamSpeed, 0, 0);
        }
    }

}
