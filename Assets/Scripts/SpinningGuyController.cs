public class SpinningGuyController : EnemyController
{
    /*void Start()
    {
        
    }*/
    
    private new void Update()
    {
        transform.Rotate(-5.0f, 0.0f, 0.0f);
        DistanceCheck();
    }
}
