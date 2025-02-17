using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadScene("Level_01"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(sceneName);
    }
}
