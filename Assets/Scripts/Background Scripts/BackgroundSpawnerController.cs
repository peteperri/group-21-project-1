using System.Collections;
using Player_Scripts;
using UnityEngine;

namespace Background_Scripts
{
    public class BackgroundSpawnerController : MonoBehaviour
    {
        [SerializeField] private GameObject bg1;
        [SerializeField] private GameObject bgTransition;
        [SerializeField] private GameObject bg2;
        private int _spawnCount;
        private PlayerController _player;
        private GameObject _bgPrefab;

        private void Start()
        {
            _bgPrefab = bg1;
            _player = FindObjectOfType<PlayerController>();
            for (_spawnCount = 0; _spawnCount < 10; _spawnCount++)
            {
                SpawnBackground();
            }
            StartCoroutine(TimerSpawn());
        }

        private void Update()
        {
            if (_spawnCount == 19)
            {
                _bgPrefab = bgTransition;
            }
            else if (_spawnCount >= 20)
            {
                _bgPrefab = bg2;
            }
        }

        private IEnumerator TimerSpawn()
        {
            while (_player.IsAlive)
            {
                SpawnBackground();
                _spawnCount++;
                yield return new WaitForSeconds(1);
            }
        }

        private void SpawnBackground()
        {
            Instantiate(_bgPrefab, transform.position + new Vector3(19.2f * _spawnCount, 0, 5), Quaternion.identity);
        }
    }
}