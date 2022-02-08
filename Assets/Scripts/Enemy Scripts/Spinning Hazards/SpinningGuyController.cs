using Player_Scripts;
using UnityEngine;

namespace Enemy_Scripts.Spinning_Hazards
{
    public class SpinningGuyController : EnemyController
    {
        private MeshRenderer _rend;
        public bool Dead { get; private set; }

        private new void Start()
        {
            Player = FindObjectOfType<PlayerController>();
            _rend = GetComponent<MeshRenderer>();
        }

        private new void Update()
        {
            transform.Rotate(-5.0f, 0.0f, 0.0f);
            DistanceCheck();
        }
        
        private new void OnTriggerEnter2D(Collider2D other)
        {
            CheckIfDamaged(other);
        }
        
        private void CheckIfDamaged(Collider2D other)
        {
            if (other.CompareTag("Shot") && !Dead)
            {
                Destroy(other.gameObject);
                _rend.enabled = false;
                gameObject.tag = "Untagged";
                Dead = true;
            }
        }
        
    }
}
