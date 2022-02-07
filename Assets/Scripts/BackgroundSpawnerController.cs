using System.Collections;
using UnityEngine;

public class BackgroundSpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject bgPrefab;
    private bool _coroutineBegun;
    private int _spawnCount;

    private void Start()
    {
        for (_spawnCount = 0; _spawnCount < 2; _spawnCount++)
        {
            Instantiate(bgPrefab, transform.position + new Vector3(19.2f * _spawnCount, 0, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        if (!_coroutineBegun)
        {
            StartCoroutine(SpawnBackground());
            _coroutineBegun = true;
        }
    }

    private IEnumerator SpawnBackground()
    {
        Instantiate(bgPrefab, transform.position + new Vector3(19.2f * _spawnCount, 0, 0), Quaternion.identity);
        _spawnCount++;
        yield return new WaitForSeconds(4);
        _coroutineBegun = false;
    }
}