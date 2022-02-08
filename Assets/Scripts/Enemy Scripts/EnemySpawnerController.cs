using System.Collections;
using UnityEngine;

namespace Enemy_Scripts
{
    public class EnemySpawnerController : MonoBehaviour
    {
        private CameraController _cameraController;
        [SerializeField] private GameObject enemyShipPrefab;
        [SerializeField] private GameObject spinnerPrefab;

        void Start()
        {
            _cameraController = FindObjectOfType<CameraController>();
            StartCoroutine(WaitAndSpawn(3));
        }
    
        void Update()
        {
            CameraFollow();
        }
    
        private void CameraFollow() //updates this object's x position to match camera's
        {
            transform.position += new Vector3(_cameraController.CamSpeed, 0, 0);
        }

        private void SpawnEnemy()
        {
            
            int enemyToSpawn = Random.Range(1,3);
            float spawnPoint;
            switch (enemyToSpawn)
            {
                case 1:
                    spawnPoint = Random.Range(-2.0f, 2.0f);
                    Instantiate(enemyShipPrefab,new Vector2(transform.position.x, spawnPoint), Quaternion.Euler(0, 0, 90));
                    break;
                case 2: 
                    spawnPoint = Random.Range(-4.0f, 4.0f);
                    Instantiate(spinnerPrefab,new Vector2(transform.position.x, spawnPoint), Quaternion.identity);
                    break;
            }
        }
    
        IEnumerator WaitAndSpawn(int time)
        {
            while (true)
            {
                int enemyCount = Random.Range(1, 4);
                for (int i = 0; i < enemyCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
                }
                yield return new WaitForSeconds(time);
            }
        }
    }
}
