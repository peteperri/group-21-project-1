using System;
using TMPro;
using UnityEngine;

namespace Enemy_Scripts.Small_Ship_Enemy
{
    public class SmallEnemyShipController : EnemyController
    {

        private int _direction;
        private new void Start()
        {
            base.Start();
            if (transform.position.y > 0)
            {
                _direction = -1;
                transform.eulerAngles += new Vector3(45, 0, 0);
            }
            else
            {
                _direction = 1;
                transform.eulerAngles += new Vector3(-45, 0, 0);
            }
        }

        private new void Update()
        {
            base.Update();
            transform.position += new Vector3(-0.01f, 0.05f * _direction,0); 
        }

        private new void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            Debug.Log("Collided");
            if (other.CompareTag("Enemy") || other.CompareTag("Obstacle") || other.CompareTag("Border"))
            {
                _direction *= -1;
                transform.eulerAngles = new Vector3(45 * -_direction, -90, 0);
            }
        }
    }
}

