using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Camera_Scripts
{
    public class UIController : MonoBehaviour
{
    [SerializeField] private Texture speedUpOff;
    [SerializeField] private Texture speedUpOn;
    [SerializeField] private Texture missileOff;
    [SerializeField] private Texture missileOn;
    [SerializeField] private Texture doubleOff;
    [SerializeField] private Texture doubleOn;
    [SerializeField] private Texture laserOff;
    [SerializeField] private Texture laserOn;
    [SerializeField] private Texture shieldOff;
    [SerializeField] private Texture shieldOn;
    
    [SerializeField] private RawImage speedUpImage;
    [SerializeField] private RawImage missileImage;
    [SerializeField] private RawImage doubleImage;
    [SerializeField] private RawImage laserImage;
    [SerializeField] private RawImage shieldImage;

    private PlayerController _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_player.PowerUpState)
        {
            case 0:
                speedUpImage.texture = speedUpOff;
                missileImage.texture = missileOff;
                doubleImage.texture = doubleOff;
                laserImage.texture = laserOff;
                shieldImage.texture = shieldOff;
                break;
            case 1:
                speedUpImage.texture = speedUpOn;
                missileImage.texture = missileOff;
                doubleImage.texture = doubleOff;
                laserImage.texture = laserOff;
                shieldImage.texture = shieldOff;
                break;
            case 2:
                missileImage.texture = missileOn;
                speedUpImage.texture = speedUpOff;
                doubleImage.texture = doubleOff;
                laserImage.texture = laserOff;
                shieldImage.texture = shieldOff;
                break;
            case 3:
                doubleImage.texture = doubleOn;
                speedUpImage.texture = speedUpOff;
                missileImage.texture = missileOff;
                laserImage.texture = laserOff;
                shieldImage.texture = shieldOff;
                break;
            case 4:
                laserImage.texture = laserOn;
                speedUpImage.texture = speedUpOff;
                missileImage.texture = missileOff;
                doubleImage.texture = doubleOff;
                shieldImage.texture = shieldOff;
                break;
            case 5:
                shieldImage.texture = shieldOn;
                speedUpImage.texture = speedUpOff;
                missileImage.texture = missileOff;
                doubleImage.texture = doubleOff;
                laserImage.texture = laserOff;
                break;
        }
    }
}

}
