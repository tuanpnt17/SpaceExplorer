using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameUIHandler.Instance.OnPauseButtonClicked();
        }
    }
}
