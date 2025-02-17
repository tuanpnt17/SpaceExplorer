using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public KeyCode shootKey = KeyCode.Space;
    private AudioManager audioManager;

	private void Awake()
	{
		audioManager = FindAnyObjectByType<AudioManager>();
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            audioManager.PlayProjectileSound();
			ShootProjectile();
		}
	}

    private void ShootProjectile()
    {
		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

		Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null) 
        {
            rb.linearVelocity = firePoint.up * projectileSpeed;   
        }
		Destroy(projectile, 1);

	}
}