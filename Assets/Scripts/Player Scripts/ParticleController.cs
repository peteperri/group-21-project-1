using UnityEngine;

namespace Player_Scripts
{
    public class ParticleController : MonoBehaviour
    {
        private PlayerController _player;
        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = _player.transform.position - new Vector3(0.94f, 0.03f, 0);
            if (!_player.IsAlive)
            {
                Destroy(gameObject);
            }
        }
    }
}

