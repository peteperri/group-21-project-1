using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;
    private PlayerController _player;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
    }
    
    private void Update() //kills projectiles when they get too far away
    {
        if (transform.position.x - _player.transform.position.x > 20.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(float force) //shoot method
    {
        rigidbody2d.AddForce(new Vector2(force, 0));
    }
}