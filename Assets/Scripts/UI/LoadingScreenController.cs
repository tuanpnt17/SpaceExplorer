using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingScreenController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
    private AsyncOperation asyncLoad;

    void Start()
    {
       
        videoPlayer.isLooping = true;  
        videoPlayer.Play();

        // StartCoroutine(LoadMainMenuAsync());

        StartCoroutine(LoadMenuAfterDelay(3f));

    }

    IEnumerator LoadMainMenuAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Menu");
        asyncLoad.allowSceneActivation = false;  

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    IEnumerator LoadMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  
        SceneManager.LoadScene("Menu");          
    }
}
