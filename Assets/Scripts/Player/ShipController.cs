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

    // Camera
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // Ship
    private float objectWidth;
    private float objectHeight;

    // health
    private float _currentHealth = 3.0f;
    private int _maxHealth = 3;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        objectWidth = spriteRenderer.bounds.extents.x;
        objectHeight = spriteRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector2(moveHorizontal, moveVertical);
        HandleSpaceShooting();
    }

    private void FixedUpdate()
    {
        //rb.linearVelocity = movement * spaceShipSpeed;
        Vector2 pos = rb.position + movement * spaceShipSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y - objectHeight * 10);
        rb.MovePosition(pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            audioManager.PlayCollectSound();
            Destroy(collision.gameObject);
            score++;
            GameUIHandler.Instance.SetStarValue(score);
            if (score == 10)
            {
                SceneFader.Instance.FadeToScene("Level_03", 0.5f);
            }
        }
        if (collision.CompareTag("Asteroid"))
        {
            audioManager.PlayExplodeSound();
            if (shield == null)
            {
                _currentHealth -= 1.0f;
                GameUIHandler.Instance.SetHealthValue(_currentHealth / (float)_maxHealth);

                if (_currentHealth <= 0)
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("EndGame");
                }
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
