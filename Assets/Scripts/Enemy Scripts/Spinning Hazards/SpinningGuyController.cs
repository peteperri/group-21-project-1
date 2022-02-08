namespace Enemy_Scripts.Spinning_Hazards
{
    public class SpinningGuyController : EnemyController
    {
        private new void Update()
        {
            transform.Rotate(-5.0f, 0.0f, 0.0f);
            DistanceCheck();
        }
    }
}
