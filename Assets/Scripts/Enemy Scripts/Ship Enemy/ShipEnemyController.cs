using UnityEngine;

namespace Enemy_Scripts.Ship_Enemy
{
    public class ShipEnemyController : EnemyController
    {
        [SerializeField] private float waveSize;
        [SerializeField] private float waveSpeed;
        [SerializeField] private float moveSpeed;

        // Update is called once per frame
        private new void Update()
        {
            DistanceCheck();
            Vector3 sinePattern = transform.right * waveSize * Mathf.Sin(Time.time * waveSpeed);
            Vector3 moveTowardsPlayer = new Vector3(moveSpeed, 0, 0);
            transform.position += (sinePattern - moveTowardsPlayer) * Time.deltaTime;
        }
    }
}
