using TMPro;
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

    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScore;

    private void Awake() { }

    void Start()
    {
        //if (currentScore == null)
        //{
        //    Debug.LogError("Không tìm thấy currentScore trong Scene!");
        //    return;
        //}
        //if (bestScore == null)
        //{
        //    Debug.LogError("Không tìm thấy bestScore trong Scene!");
        //    return;
        //}
        currentScore.SetText("10");
        bestScore.SetText("20");
    }

    public void AddScore(int amount)
    {
        //currentScore += amount;
        //if (currentScore > bestScore)
        //{
        //    bestScore = currentScore;
        //    PlayerPrefs.SetInt("BestScore", bestScore);
        //    PlayerPrefs.Save();
        //}
        //UpdateScoreUI();
    }

    //private void UpdateScoreUI()
    //{
    //    if (currentScoreText != null)
    //        currentScoreText.text = "Score: " + currentScore;

    //    if (bestScoreText != null)
    //        bestScoreText.text = "Best Score: " + bestScore;
    //}
}
