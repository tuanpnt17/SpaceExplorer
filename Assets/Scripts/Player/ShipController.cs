using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float spaceShipSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float projectileSpeed = 10f;
    private AudioManager audioManager;
    public int score = 0;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject shieldPrefab;
    private GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleSpaceShooting();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * spaceShipSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            audioManager.PlayCollectSound();
            Destroy(collision.gameObject);
            score++;
        }
        if (collision.CompareTag("Asteroid"))
        {
            audioManager.PlayExplodeSound();
            if (shield == null)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }
        }
        if (collision.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            shield = Instantiate(shieldPrefab, gameObject.transform);
            shield.transform.localPosition = new Vector2(0, 0.198f);
            Destroy(shield, 3f);
        }
    }

    private void HandleSpaceShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlayProjectileSound();
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.up * projectileSpeed;
        }
        Destroy(projectile, 1);
    }
}
