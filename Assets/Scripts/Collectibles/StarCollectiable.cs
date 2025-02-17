using System;
using UnityEngine;

public class StarCollectiable : MonoBehaviour
{
    public float speed;
    public string idleAnimationName;

    private Animator animator;
    private GameSpawner spawnerInstance;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveStar();

        if (transform.position.y < -Camera.main.orthographicSize - 1f)
        {
            DestroyStar();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawnerInstance.HandleStarCollected();
            DestroyStar();
        }
    }

    public void SetStartTime(float normalizedStartTime)
    {
        if (animator != null)
        {
            animator.Play(idleAnimationName, -1, normalizedStartTime);
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetSpawner(GameSpawner spawner)
    {
        if (spawnerInstance == null)
            spawnerInstance = spawner;
    }

    private void MoveStar()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void DestroyStar()
    {
        spawnerInstance.HandleStarDestroyed(gameObject);
    }
}
