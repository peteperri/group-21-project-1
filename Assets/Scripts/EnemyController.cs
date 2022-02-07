using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health = 1;
    private PlayerController _player;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (_player.transform.position.x - transform.position.x  > 10.0f)
        {
            Destroy(gameObject);
        }
    }
    
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

