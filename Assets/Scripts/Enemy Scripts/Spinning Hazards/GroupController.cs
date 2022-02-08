using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Enemy_Scripts.Spinning_Hazards
{
    public class GroupController : MonoBehaviour
    {
        [SerializeField] private SpinningGuyController child1;
        [SerializeField] private SpinningGuyController child2;
        [SerializeField] private SpinningGuyController child3;
        [SerializeField] private SpinningGuyController child4;
        [SerializeField] private GameObject pickup;
        [SerializeField] private float child1speed;
        [SerializeField] private float delay;
        private readonly SpinningGuyController[] _children = new SpinningGuyController[4];
        private Camera _camera;
        private bool _leavingScreen;

        private void Start()
        {
            _camera = Camera.main;
            _children[0] = child1;
            _children[1] = child2;
            _children[2] = child3;
            _children[3] = child4;
        }

        private void Update()
        {
            int deadCount = 0;
            foreach (SpinningGuyController child in _children)
            {
                if (child.Dead)
                {
                    deadCount++;
                }
            }

            if (deadCount == _children.Length)
            {
                Instantiate(pickup, transform.position + new Vector3(0f, 0, 0), Quaternion.identity);
                Destroy(gameObject);
            }

            Vector3 relativePosition =
                _camera.transform.InverseTransformDirection(
                    _children[0].transform.position - _camera.transform.position);
            if (relativePosition.x < 2.0f)
            {
                _leavingScreen = true;
            }

            if (_leavingScreen)
            {
                if (_children[0].transform.position.y > 0)
                {
                    _children[0].transform.position += new Vector3(child1speed * 2, child1speed, 0);
                }
                else
                {
                    _children[0].transform.position += new Vector3(child1speed * 2, -child1speed, 0);
                }
                for (int i = 1; i < _children.Length; i++)
                {
                    _children[i].transform.position = Vector3.MoveTowards(_children[i].transform.position,
                        _children[i - 1].transform.position, delay);
                }
            }

        }
    }
}
