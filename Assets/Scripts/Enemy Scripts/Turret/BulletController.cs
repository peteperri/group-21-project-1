using UnityEngine;

namespace Enemy_Scripts.Turret
{
    public class BulletController : MonoBehaviour
    {
        private void Update()
        {
            transform.eulerAngles += new Vector3(0,0,3600) * Time.deltaTime;
        }
    }
}