using UnityEngine;
using UnityEngine.SceneManagement;

public class FeatureMenu : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Score() { }
}
