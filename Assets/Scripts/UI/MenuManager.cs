using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject instructionUI;
    public TextMeshProUGUI HighestScore;

    //private ScoreManager scoreManager;

    void Start()
    {
        //scoreManager = FindAnyObjectByType<ScoreManager>(); // Tự động tìm ScoreManager

        //if (scoreManager == null)
        //{
        //    Debug.LogError("Không tìm thấy ScoreManager trong Scene!");
        //    return;
        //}

        // Giả sử người chơi đạt 120 điểm
        //int currentScore = 120;
        //scoreManager.SaveBestScore(currentScore);
        //int bestScore = scoreManager.LoadBestScore();
        int bestScore = ScoreManager.Instance.LoadBestScore();
        Debug.Log("Best Score: " + bestScore);

        HighestScore.text = bestScore.ToString();
    }

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
