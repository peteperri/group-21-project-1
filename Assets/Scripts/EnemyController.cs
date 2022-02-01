using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField ]private int health = 3;
    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckIfDamaged(other);
    }
    
    private void CheckIfDamaged(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            health--;
            Destroy(other.gameObject);
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
