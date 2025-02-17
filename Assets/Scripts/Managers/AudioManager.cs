using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundSource;
	[SerializeField] private AudioSource effectSource;
	[SerializeField] private AudioClip backgroudClip;
	[SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip collectClip;
	[SerializeField] private AudioClip explodeClip;

	// Start is called before the first frame update
	void Start()
    {
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayBackgroundMusic()
    {
        backgroundSource.loop = true;
        backgroundSource.clip = backgroudClip;
        backgroundSource.Play();
    }

    public void PlayProjectileSound()
    {
        effectSource.PlayOneShot(shootClip);
    }

    public void PlayCollectSound()
    {
        effectSource.PlayOneShot(collectClip);
    }

	public void PlayExplodeSound()
	{
		effectSource.PlayOneShot(explodeClip);
	}

}
