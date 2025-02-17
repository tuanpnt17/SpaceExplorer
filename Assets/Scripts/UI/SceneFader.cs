using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }
    public CanvasGroup canvasGroup; // CanvasGroup trên Canvas fade overlay

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Gọi hàm này để thực hiện chuyển scene với hiệu ứng fade mượt
    public void FadeToScene(string sceneName, float fadeDuration)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName, fadeDuration));
    }

    IEnumerator FadeAndSwitchScene(string sceneName, float fadeDuration)
    {
        // Fade Out: tăng alpha từ 0 đến 1 mượt mà
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Load scene mới (sceneName phải khớp với tên trong Build Settings)
        SceneManager.LoadScene(sceneName);
        yield return null;

        // Fade In: giảm alpha từ 1 xuống 0 mượt mà
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
