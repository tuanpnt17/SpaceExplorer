using UnityEngine;

public class StarGen : MonoBehaviour
{
    public GameObject Star;
    public int MaxStars;

    private Color[] starColor = { Color.white, Color.yellow, Color.red, Color.gray };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MaxStars <= 0)
        {
            MaxStars = 10;
        }
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        for (int i = 0; i < MaxStars; i++)
        {
            GameObject star = Instantiate(Star);
            star.GetComponent<SpriteRenderer>().color = starColor[i % starColor.Length];
            star.transform.position = new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y)
            );
            star.GetComponent<Star>().speed = Random.Range(0.1f, 0.9f);
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update() { }
}
