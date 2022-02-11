using System.Collections;
using Enemy_Scripts.Boss;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Camera_Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float camSpeed;
        private BossController _boss;
        public float CamSpeed { get => camSpeed; set => camSpeed = value; } 

        private void Start()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            _boss = FindObjectOfType<BossController>();
        }

        private void Update()
        {
            MoveCamera();
            if (_boss == null)
            {
                StartCoroutine(LoadWinScene());
            }
        }

        private void MoveCamera() //moves the camera across the game world at a rate of camSpeed per frame
        {
            transform.position += new Vector3(CamSpeed, 0, 0);
        }

        private IEnumerator LoadWinScene()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(2);
        }
    }

}
