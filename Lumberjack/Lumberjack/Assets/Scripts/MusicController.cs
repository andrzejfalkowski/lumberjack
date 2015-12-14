using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour 
{
	static MusicController _instance;
	static public MusicController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(MusicController)) as MusicController;
			}
			return _instance;
		}
	}

	public AudioClip GamePlayMusic;
	public AudioClip MenuMusic;
	private AudioSource audioSource;


	// Use this for initialization
	void Awake () 
	{
		if(audioSource == null)
			audioSource = GetComponent<AudioSource>();
	}

	public void PlayGamePlay()
	{
		audioSource.clip = GamePlayMusic;
		audioSource.Play();
	}

	public void PLayMenu()
	{
		audioSource.clip = MenuMusic;
		audioSource.Play();
	}

	public void Play()
	{
		audioSource.Play();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
