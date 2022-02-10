using System.Collections;
using Player_Scripts;
using UnityEngine;

namespace Background_Scripts
{
    public class BackgroundSpawnerController : MonoBehaviour
    {
        [SerializeField] private GameObject bgPrefab;
        private int _spawnCount;
        private PlayerController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            for (_spawnCount = 0; _spawnCount < 10; _spawnCount++)
            {
                SpawnBackground();
            }
            StartCoroutine(TimerSpawn());
        }

        private IEnumerator TimerSpawn()
        {
            while (_player.IsAlive)
            {
                SpawnBackground();
                _spawnCount++;
                yield return new WaitForSeconds(2);
            }
        }

        private void SpawnBackground()
        {
            Instantiate(bgPrefab, transform.position + new Vector3(19.2f * _spawnCount, 0, 0), Quaternion.identity);
        }
    }
}