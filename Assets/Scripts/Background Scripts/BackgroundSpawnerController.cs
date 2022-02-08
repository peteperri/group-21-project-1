using System.Collections;
using UnityEngine;

namespace Background_Scripts
{
    public class BackgroundSpawnerController : MonoBehaviour
    {
        [SerializeField] private GameObject bgPrefab;
        private int _spawnCount;

        private void Start()
        {
            for (_spawnCount = 0; _spawnCount < 2; _spawnCount++)
            {
                SpawnBackground();
            }
            StartCoroutine(TimerSpawn());
        }

        private IEnumerator TimerSpawn()
        {
            while (true)
            {
                SpawnBackground();
                _spawnCount++;
                yield return new WaitForSeconds(4);
            }
        }

        private void SpawnBackground()
        {
            Instantiate(bgPrefab, transform.position + new Vector3(19.2f * _spawnCount, 0, 0), Quaternion.identity);
        }
    }
}