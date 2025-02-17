using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    void Start()
    {
        //if (audioSource == null)
        //{
        //    audioSource = GetComponent<AudioSource>();
        //}

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            Debug.Log("?? ?ang phát âm thanh click!");
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogError("? L?i: AudioSource ho?c ClickSound ch?a ???c gán!");
        }
    }
}
