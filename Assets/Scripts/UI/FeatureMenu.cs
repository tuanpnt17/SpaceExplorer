using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeatureMenu : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScore;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake() { }

    void Start()
    {
        int score = ScoreManager.Instance.currentScore;
        ScoreManager.Instance.SaveBestScore(score);
        currentScore.text = score.ToString();
        bestScore.text = ScoreManager.Instance.LoadBestScore().ToString();
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
