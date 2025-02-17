using UnityEngine;

public class TripleShotEngine : MonoBehaviour
{
    [HideInInspector]
    public Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
    {
    }

    public void TripleShotEnd()
    {
        _animator.SetBool("IsShooting", false);
    }
}