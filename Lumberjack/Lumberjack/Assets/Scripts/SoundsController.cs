using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundsController : MonoBehaviour 
{
	private static SoundsController instance = null;
	public static SoundsController Instance
	{
		get
		{ 
			return instance; 
		}
	}
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}		
		instance = this;
		DontDestroyOnLoad( this.gameObject );
	}

	public List<AudioClip> AnnouncementSoundList;
	public AudioClip LauncherClip;

	public AudioSource AnnouncementSoundSource;
	public AudioSource ChopSoundSource;

	public void PlayAnnouncement(int sfx)
	{
		AnnouncementSoundSource.clip = AnnouncementSoundList[(int)sfx];
		AnnouncementSoundSource.Play();
	}

	public void PlayChopSound()
	{
		ChopSoundSource.Play();
	}

	public void PlayLauncherSound()
	{
		AudioSource audioSrc = this.gameObject.AddComponent<AudioSource>();
		audioSrc.clip = LauncherClip;
		audioSrc.Play();
		Destroy(audioSrc, 3f);
	}

	public void PlayAnnouncementSoundFromClip(AudioClip clip)
	{
		AnnouncementSoundSource.PlayOneShot(clip);
	}
}
