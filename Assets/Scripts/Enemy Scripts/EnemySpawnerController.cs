using System.Collections;
using Player_Scripts;
using Camera_Scripts;
using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemySpawnerController : MonoBehaviour
    {
        private CameraController _cameraController;
        private PlayerController _player;
        private int _time;
        [SerializeField] private GameObject enemyShipPrefab;
        [SerializeField] private GameObject spinnerPrefab;
        [SerializeField] private GameObject hourGlassPrefab;

        void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            _cameraController = FindObjectOfType<CameraController>();
            _time = 3;
            StartCoroutine(WaitAndSpawn());
        }
    
        void Update()
        {
            CameraFollow();
        }
    
        private void CameraFollow() //updates this object's x position to match camera's
        {
            transform.position += new Vector3(_cameraController.CamSpeed, 0, 0) * Time.deltaTime;
        }

        private void SpawnEnemy()
        {
            int enemyToSpawn = Random.Range(1,4);
            if (transform.position.x > 300)
            {
                _time = 5;
                enemyToSpawn = 1;
            }

            float spawnPoint = Random.Range(-4.0f, 4.0f);;
            switch (enemyToSpawn)
            {
                case 1:
                    Instantiate(spinnerPrefab,new Vector2(transform.position.x, spawnPoint), Quaternion.identity);
                    break;
                case 2: 
                    Instantiate(hourGlassPrefab,new Vector2(transform.position.x, spawnPoint), Quaternion.identity);
                    break;
                default: 
                    spawnPoint = Random.Range(-2.0f, 2.0f);
                    Instantiate(enemyShipPrefab,new Vector2(transform.position.x, spawnPoint), Quaternion.Euler(0, 0, 90));
                    break;
            }
        }
    
        IEnumerator WaitAndSpawn()
        {
            while (_player.IsAlive)
            {
                int enemyCount = Random.Range(1, 3);
                for (int i = 0; i < enemyCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
                }
                yield return new WaitForSeconds(_time);
            }
        }
    }
}
