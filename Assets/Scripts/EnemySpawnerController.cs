using System.Collections;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    private CameraController _cameraController;
    [SerializeField] private GameObject enemyPrefab;
    private bool _coroutineBegun;
    
    void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }
    
    void Update()
    {
        CameraFollow();
        if (!_coroutineBegun)
        {
            StartCoroutine(WaitAndSpawn(3));
            _coroutineBegun = true;
        }
    }
    
    private void CameraFollow() //updates this object's x position to match camera's
    {
        transform.position += new Vector3(_cameraController.CamSpeed, 0, 0);
    }

    private void SpawnEnemy()
    {
        float spawnPoint = Random.Range(-4.0f, 4.0f);
        Instantiate(enemyPrefab,new Vector2(transform.position.x, spawnPoint), Quaternion.identity);
    }
    
    IEnumerator WaitAndSpawn(int time)
    {
        
        int enemyCount = Random.Range(1, 4);
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
        }
        yield return new WaitForSeconds(time);
        _coroutineBegun = false;
    }
}
