using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AsteroidController : MonoBehaviour
{
    public float damagePerHit = 0.5f;
    private float currentHealth;
    private Animator animator;

    public GameObject explosionSoundPrefab;

    private float rotationSpeed;

    private float speed;
    private Vector2 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = transform.localScale.x; // set health equal to scale of asteroid
    }

    void Update()
    {
        RotateAsteroid();
        MoveAsteroid();

        if (transform.position.y < -Camera.main.orthographicSize - 1f) // destroy asteroid if it goes off-screen
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // change to spaceship
        //SpaceshipDemo player = other.gameObject.GetComponent<SpaceshipDemo>();
        if (other.CompareTag("Player"))
        {
            Explode();
            return;
        }

        //ProjectileDemo projectile = other.gameObject.GetComponent<ProjectileDemo>();
        if (other.CompareTag("🚀"))
        {
            Destroy(other.gameObject);
			currentHealth -= damagePerHit;
            if (currentHealth <= 0)
            {
                Explode();
            }
        }

    }

    public void ExplosionDone()
    {
        Destroy(gameObject);
    }

    public void SetRotationSpeed(float rs)
    {
        rotationSpeed = rs;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Explode()
    {
        GameObject soundObject = Instantiate(
            explosionSoundPrefab,
            transform.position,
            Quaternion.identity
        );
        Destroy(soundObject, soundObject.GetComponent<AudioSource>().clip.length);
        animator.SetTrigger("Explore");
    }

    private void RotateAsteroid()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void MoveAsteroid()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
