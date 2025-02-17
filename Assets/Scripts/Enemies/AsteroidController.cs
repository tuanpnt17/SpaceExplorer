using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AsteroidController : MonoBehaviour
{
    public float damagePerHit = 0.5f;
    public GameObject explosionSoundPrefab;

    private GameSpawner spawnerInstance;
    private float rotationSpeed;
    private float speed;
    private Vector2 moveDirection;
    private float currentHealth;
    private Animator animator;
    private Collider2D collider2d;

    void Awake()
    {
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        RotateAsteroid();
        MoveAsteroid();

        if (transform.position.y < -Camera.main.orthographicSize - 1f) // destroy asteroid if it goes off-screen
        {
            DestroyAsteroid();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
            return;
        }

        if (other.CompareTag("Projectile"))
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
        DestroyAsteroid();
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

    public void SetSpawner(GameSpawner spawner)
    {
        if (spawnerInstance == null)
            spawnerInstance = spawner;
    }

    public void StartForObjectPooling()
    {
        collider2d.enabled = true;
        animator.Play("Default");
        currentHealth = transform.localScale.x; // set health equal to scale of asteroid
    }

    private void Explode()
    {
        collider2d.enabled = false;
        GameObject soundObject = Instantiate(
            explosionSoundPrefab,
            transform.position,
            Quaternion.identity
        );
        Destroy(soundObject, soundObject.GetComponent<AudioSource>().clip.length);
        animator.SetTrigger("Explore");
        spawnerInstance.ExplodeAsteroid(transform.position, transform.localScale.x);
    }

    private void RotateAsteroid()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void MoveAsteroid()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }

    private void DestroyAsteroid()
    {
        spawnerInstance.HandleAsteroidDestroyed(gameObject);
    }
}
