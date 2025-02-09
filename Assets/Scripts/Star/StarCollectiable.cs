using System;
using UnityEngine;

public class StarCollectiable : MonoBehaviour
{
    public GameObject collectibleSound;
    public float speed;
    public string idleAnimationName;

    private event Action OnDestroyed;
    private Animator animator;

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
        // change to spaceship
        //SpaceshipDemo controller = other.GetComponent<SpaceshipDemo>();
        //if (controller != null)
        //{
        //    GameObject soundObject = Instantiate(
        //        collectibleSound,
        //        transform.position,
        //        Quaternion.identity
        //    );
        //    Destroy(soundObject, soundObject.GetComponent<AudioSource>().clip.length);
        //    Debug.Log("Star collected!"); // change to update the score
        //    DestroyStar();
        //}
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

    public void AddOnDestroyedListener(Action listener)
    {
        OnDestroyed = listener;
    }

    private void MoveStar()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void DestroyStar()
    {
        OnDestroyed?.Invoke();
        gameObject.SetActive(false);
    }
}
