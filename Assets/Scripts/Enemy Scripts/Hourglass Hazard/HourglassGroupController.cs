using UnityEngine;

namespace Enemy_Scripts.Hourglass_Hazard
{
    public class HourglassGroupController : EnemyController
    {
        private new void Update()
        {
            DistanceCheck();
            transform.position += new Vector3(-5f, 0, 0) * Time.deltaTime;
        }
    }
}

