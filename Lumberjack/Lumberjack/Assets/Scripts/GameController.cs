using UnityEngine;
using System.Collections;

public enum EGamePhase
{
	Prepare,
	InProgress,
	GameOver
}

public class GameController : MonoBehaviour 
{
	static GameController _instance;
	static public GameController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameController)) as GameController;
			}
			return _instance;
		}
	}

	public EGamePhase CurrentGamePhase = EGamePhase.Prepare;

	public ControllableCharacter MainCharacter;
	public Collider2D PlayableArea;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame()
	{
		CurrentGamePhase = EGamePhase.InProgress;
	}

	public void EndGame()
	{
		CurrentGamePhase = EGamePhase.GameOver;
	}
}
