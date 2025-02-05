using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : MonoBehaviour
{
    public int score = 0;
	public GameObject shieldPrefab;
	private GameObject shield;
    private AudioManager audioManager;

	private void Awake()
	{
		audioManager = FindAnyObjectByType<AudioManager>();
	}

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
            Destroy(gameObject);
        }
		if (collision.CompareTag("Shield"))
		{
			Destroy(collision.gameObject);
			shield = Instantiate(shieldPrefab, gameObject.transform);
			shield.transform.localPosition = new Vector2(0, 0.198f);
			Destroy(shield, 3f);
		}

	}
}