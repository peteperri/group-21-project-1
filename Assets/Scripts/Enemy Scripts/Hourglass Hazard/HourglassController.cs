using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Enemy_Scripts;
using Player_Scripts;
using UnityEngine;

namespace Enemy_Scripts.Hourglass_Hazard
{
    public class HourglassController : EnemyController
    {
        private new void Update()
        {
            DistanceCheck();
            transform.Rotate(0,-360 * Time.deltaTime,0);
        }
    }
}

