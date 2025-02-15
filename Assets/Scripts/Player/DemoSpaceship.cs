using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DemoSpaceship : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction MoveAction;
    public float Speed;
    public int Score;
    private bool transitionedToLevel2 = false;
    private bool transitionedToLevel3 = false;

    void Start()
    {
        MoveAction.Enable();
        Speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();

        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float objectWidth = spriteRenderer.bounds.extents.x / 2;
        float objectHeight = spriteRenderer.bounds.extents.y / 2;

        Vector2 pos = (Vector2)transform.position + move * Time.deltaTime * Speed;

        pos.x = Mathf.Clamp(pos.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);

        transform.position = pos;

        // Giả sử điểm số được cập nhật trong quá trình chơi (ở đây chỉ demo tăng điểm theo thời gian)
        Score += Mathf.FloorToInt(Time.deltaTime * 10); // Ví dụ tăng điểm

        // Khi đạt 100 điểm và đang ở Level_01, chuyển sang Level_02
        if (
            Score >= 100
            && SceneManager.GetActiveScene().name == "Level_01"
            && !transitionedToLevel2
        )
        {
            transitionedToLevel2 = true;
            // Sử dụng fade transition với thời gian fade duration 1.5s
            SceneFader.Instance.FadeToScene("Level_02", 0.5f);
        }

        //// Khi đạt 1000 điểm và đang ở Level_02, chuyển sang Level_03 với hiệu ứng chuyển nhanh hơn (fadeDuration ngắn hơn)
        //if (Score >= 1000 && SceneManager.GetActiveScene().name == "Level_02" && !transitionedToLevel3)
        //{
        //    transitionedToLevel3 = true;
        //    // Ví dụ fadeDuration là 0.75s để chuyển nhanh hơn
        //    SceneFader.Instance.FadeToScene("Level_03", 0.75f);
        //}
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
