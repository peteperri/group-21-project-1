using Player_Scripts;
using UnityEngine;

namespace Background_Scripts
{
    public class BackgroundController : MonoBehaviour
    {
        private PlayerController _player;
        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            if (_player.transform.position.x - transform.position.x  > 30.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
