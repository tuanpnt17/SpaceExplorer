using UnityEngine;

public class BkgAudioManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
