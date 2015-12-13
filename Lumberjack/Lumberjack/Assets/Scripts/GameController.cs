using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EGamePhase
{
	Menu,
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

	public EGamePhase CurrentGamePhase = EGamePhase.Menu;

	public ControllableCharacter MainCharacter;
	public Collider2D PlayableArea;

	public List<EnemyTree> SpawnedTrees = new List<EnemyTree>();

	[SerializeField]
	GameObject gameplayObject;
	[SerializeField]
	GameObject mainMenuObject;

	// Use this for initialization
	void Start () 
	{
		mainMenuObject.SetActive(true);
		gameplayObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void StartGame()
	{
		SpawnedTrees.Clear();

		mainMenuObject.SetActive(false);
		gameplayObject.SetActive(true);

		CurrentGamePhase = EGamePhase.InProgress;
	}

	public void EndGame()
	{
		CurrentGamePhase = EGamePhase.GameOver;
	}
}
