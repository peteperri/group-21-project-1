using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_Scripts
{
    public class EndScene : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                RestartGame();
            }
        }

        public void MainMenu ()
        {
            SceneManager.LoadScene(0);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame ()
        {
            Application.Quit();
        }
    }

}

