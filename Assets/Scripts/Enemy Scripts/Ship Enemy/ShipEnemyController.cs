using UnityEngine;

namespace Enemy_Scripts.Ship_Enemy
{
    public class ShipEnemyController : EnemyController
    {
        [SerializeField] private float waveSize;
        [SerializeField] private float speed;

        // Update is called once per frame
        private new void Update()
        {
            DistanceCheck();
            transform.position += transform.right *  Time.deltaTime * waveSize * Mathf.Sin (Time.time * speed);
        }
    }
}
