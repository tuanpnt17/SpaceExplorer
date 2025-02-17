using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float spaceShipSpeed = 5f;

    [SerializeField]
    private float fireRate = 0.3f;

    [SerializeField]
    private float projectileSpeed = 10f;
    public int score = 0;
    public int hp = 3;

    private Rigidbody2D rb;
    private Vector2 movement;

    private bool isTripleShot = false;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private Transform enginePoint;
    TripleShotEngine _tripleShotEngine;

    [SerializeField]
    private GameObject tripleShotEnginePrefab;
    private GameObject tripleShotEngine;

    private float nextFireTime = 0f;

    [SerializeField]
    private GameObject projectilePrefab;

    private AudioManager audioManager;

    [SerializeField]
    private GameObject shieldPrefab;
    private GameObject shield;

    [SerializeField]
    private GameObject shipExplosionPrefab;

    // Camera
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // Ship
    private float objectWidth;
    private float objectHeight;

    // health
    private float _currentHealth = 3.0f;
    private int _maxHealth = 3;

    private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        StartCoroutine(TripleShotPowerUp());
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
        HandleHp();
        HandleMovement();
        HandleSpaceShooting();
    }

    public void GainHP(int amount)
    {
        hp = Mathf.Clamp(hp + amount, 0, 3);
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);
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
        if (collision.CompareTag("⭐"))
        {
            audioManager.PlayCollectSound();
            Destroy(collision.gameObject);
            score++;
        }
        if (collision.CompareTag("☄️"))
        {
            audioManager.PlayExplodeSound();

            if (shield == null)
            {
                GainHP(-1);
                Instantiate(shipExplosionPrefab, transform.position, Quaternion.identity);
                transform.position = new Vector2(1000, 1000);
                StartCoroutine(Respawn());

                _currentHealth -= 1.0f;
                GameUIHandler.Instance.SetHealthValue(_currentHealth / (float)_maxHealth);

                if (_currentHealth <= 0)
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("EndGame");
                }
            }
        }
        if (collision.CompareTag("🛡️"))
        {
            Destroy(collision.gameObject);
            ShieldBuff();
        }
        if (collision.CompareTag("❤️"))
        {
            Destroy(collision.gameObject);
            GainHP(1);
        }
        if (collision.CompareTag("🚀🚀🚀"))
        {
            StartCoroutine(TripleShotPowerUp());
        }
    }

    private IEnumerator TripleShotPowerUp()
    {
        tripleShotEngine = Instantiate(tripleShotEnginePrefab, enginePoint.transform);
        _tripleShotEngine = tripleShotEngine.GetComponent<TripleShotEngine>();
        isTripleShot = true;
        yield return new WaitForSeconds(5);
        Destroy(tripleShotEngine);
        isTripleShot = false;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        transform.position = spawnPoint;
        ShieldBuff();
    }

    private void HandleHp()
    {
        if (hp == 0)
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    private void HandleSpaceShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Set next allowed fire time
            audioManager.PlayProjectileSound();

            if (isTripleShot)
            {
                _tripleShotEngine._animator.SetBool("IsShooting", true);
                TripleShot();
            }
            else
            {
                Shot();
            }
        }
    }

    private void ShieldBuff()
    {
        shield = Instantiate(shieldPrefab, gameObject.transform);
        shield.transform.localPosition = new Vector2(0, 0.198f);
        Destroy(shield, 3f);
    }

    private void TripleShot()
    {
        // Fire three projectiles with different angles
        FireProjectile(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 15)); // Right
        FireProjectile(firePoint.position, firePoint.rotation); // Center
        FireProjectile(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -15)); // Left
    }

    private void Shot()
    {
        // Normal single shot
        FireProjectile(firePoint.position, firePoint.rotation);
    }

    private void FireProjectile(Vector2 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = rotation * Vector2.up * projectileSpeed;
        }
        Destroy(projectile, 1);
    }
}
