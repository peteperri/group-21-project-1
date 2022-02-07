using UnityEngine;

public class GroupController : MonoBehaviour
{
    [SerializeField] private SpinningGuyController child1;
    [SerializeField] private SpinningGuyController child2;
    [SerializeField] private SpinningGuyController child3;
    [SerializeField] private SpinningGuyController child4;
    [SerializeField] private GameObject pickup;
    private CameraController _cameraController;
    private readonly SpinningGuyController[] _children = new SpinningGuyController[4];
    
    private void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        _children[0] = child1;
        _children[1] = child2;
        _children[2] = child3;
        _children[3] = child4;
    }
    
    private void Update()
    {
        CameraFollow();
        
        int nullCount = 0;
        foreach (SpinningGuyController child in _children)
        {
            if (child == null)
            {
                nullCount++;
            }
        }

        if (nullCount == _children.Length)
        {
            Instantiate(pickup, transform.position + new Vector3(5f, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    private void CameraFollow() //updates this object's x position to match camera's
    {
        transform.position += new Vector3(_cameraController.CamSpeed, 0, 0);
    }
}
