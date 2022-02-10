using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Enemy_Scripts;
using Player_Scripts;
using UnityEngine;

namespace Enemy_Scripts.Hourglass_Hazard
{
    public class HourglassGroupController : EnemyController
    {
        private new void Update()
        {
            DistanceCheck();
            transform.position += new Vector3(-0.075f, 0, 0);
        }
    }
}

