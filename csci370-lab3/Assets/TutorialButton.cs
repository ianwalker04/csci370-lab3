using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
