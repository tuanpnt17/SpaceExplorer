using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject instructionUI;
    public TextMeshProUGUI HighestScore ;
    public void OnInstructionPress()
    {
        instructionUI.SetActive(true);
    }
    public void OnInstructionClose()
    {
        instructionUI.SetActive(false);

    }
    public void OnPlayPress()
    {
        //SceneManager.LoadScene("GameScene");
    }
    public void OnExitPress()
    {
        Application.Quit();
    }
    public void SetHighestScore(TextMeshProUGUI s)
    {
     HighestScore = s;

    }
}
