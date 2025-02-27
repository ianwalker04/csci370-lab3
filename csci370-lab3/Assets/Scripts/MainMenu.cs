using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
