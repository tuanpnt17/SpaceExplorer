using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    private VisualElement _healthBar;
    private Label _starScore;
    private Button _pauseButton;

    private VisualElement _pauseOverlay;
    private Button _resumeButton;
    private Button _returnMainMenuButton;
    public static GameUIHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument document = GetComponent<UIDocument>();
        VisualElement root = document.rootVisualElement;
        _healthBar = root.Q<VisualElement>("HealthBar");
        _starScore = root.Q<Label>("StarScore");
        _pauseButton = root.Q<Button>("PauseButton");
        _pauseOverlay = root.Q<VisualElement>("PauseOverlay");
        _resumeButton = root.Q<Button>("ResumeBtn");
        _returnMainMenuButton = root.Q<Button>("ExitBtn");

        _pauseButton.clickable.clicked += OnPauseButtonClicked;

        _resumeButton.clickable.clicked += OnResumeButtonClicked;
        _returnMainMenuButton.clickable.clicked += OnReturnMainMenuButtonClicked;

        _pauseOverlay.style.display = DisplayStyle.None;
        SetHealthValue(1.0f);
        SetStarValue(0);
    }

    private void OnReturnMainMenuButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }

    private void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        _pauseOverlay.style.display = DisplayStyle.None;
    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
        _pauseOverlay.style.display = DisplayStyle.Flex;
    }

    public void SetHealthValue(float percentage)
    {
        _healthBar.style.width = Length.Percent(percentage * 100.0f);
    }

    public void SetStarValue(int score)
    {
        _starScore.text = score.ToString();
    }
}
