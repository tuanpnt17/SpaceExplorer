using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float speed;

    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    private void Move()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
