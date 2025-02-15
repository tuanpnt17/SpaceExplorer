using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject instructionUI;
    public TextMeshProUGUI HighestScore;

    public void OnInstructionPress()
    {
        Debug.Log("Instruction");
        instructionUI.SetActive(true);
    }

    public void OnInstructionClose()
    {
        instructionUI.SetActive(false);
    }

    public void OnPlayPress()
    {
        Debug.Log("OnPlayPress");
        SceneManager.LoadScene("Level_01");
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
