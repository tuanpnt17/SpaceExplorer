using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : MonoBehaviour
{
    public int score = 0;
	public int hp = 3;
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

	}
}