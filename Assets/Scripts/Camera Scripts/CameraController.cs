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
        private AudioSource _bossDead;
        private bool _coroutineBegun;
        public float CamSpeed { get => camSpeed; set => camSpeed = value; } 

        private void Start()
        {
            _boss = FindObjectOfType<BossController>();
            _bossDead = GetComponent<AudioSource>();
        }

        private void Update()
        {
            MoveCamera();
            
            if (_boss == null && !_coroutineBegun)
            {
                StartCoroutine(LoadWinScene());
            }
            
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void MoveCamera() //moves the camera across the game world at a rate of camSpeed per frame
        {
            transform.position += new Vector3(CamSpeed, 0, 0) * Time.deltaTime;
        }

        private IEnumerator LoadWinScene()
        {
            _coroutineBegun = true;
            _bossDead.Play();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(2);
        }
    }

}
