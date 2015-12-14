using UnityEngine;
using UnityEngine.UI;
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
	public List<TreeSpawnPoint> TreeSpawnPoints = new List<TreeSpawnPoint>();

	[SerializeField]
	GameObject gameplayPrefab;
	public GameObject GameplayObject;
	[SerializeField]
	GameObject hudObject;
	[SerializeField]
	GameObject mainMenuObject;
	[SerializeField]
	GameObject gameOverObject;

	public Image HealthBar;
	public Text PointsLabel;
	public Text LostLabel;
	private int points = 0;
	public int Points
	{
		get { return points;}
		set 
		{ 
			points = value;
			if(points == 0)
				MyKillCount.Show("","");
			else if(points == 1)
				MyKillCount.Show("FIRST SAP!", "1 KILL");
			else if(points == 10)
				MyKillCount.Show("EFFECT AXE!", "10 KILLS");
			else if(points == 20)
				MyKillCount.Show("TREEMENDOUS!", "20 KILLS");
			else if(points == 30)
				MyKillCount.Show("EXTREEMINATOR!", "30 KILLS");
			else if(points == 40)
				MyKillCount.Show("RUN, FOREST, RUN!", "40 KILLS");
			else if(points == 50)
				MyKillCount.Show("LEAF NO TREE ALIVE!", "50 KILLS");
			else if(points == 60)
				MyKillCount.Show("UNSTUMPABLE!", "60 KILLS");
			else if(points == 70)
				MyKillCount.Show("WOODICROUS!", "70 KILLS");
			else if(points == 80)
				MyKillCount.Show("CHOPPING SPREE!", "80 KILLS");
			else if(points == 90)
				MyKillCount.Show("WE'VE RUN OUT OF PUNS!", "90 KILLS");
			else if(points == 100)
				MyKillCount.Show("PAPER INDUSTRY!", "100 KILLS");
			else if(points == 200)
				MyKillCount.Show("OZON HOLE!", "200 KILLS");
			else if(points == 500)
				MyKillCount.Show("GLOBAL WARMING!", "500 KILLS");
		}
	}

	public KillCount MyKillCount;

	public bool LPM = true;
	public bool RPM = true;

	// Use this for initialization
	void Start () 
	{
		DestroyImmediate(GameplayObject);

		hudObject.SetActive(false);
		mainMenuObject.SetActive(true);
		//gameplayObject.SetActive(false);
		gameOverObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		LPM = Input.GetMouseButton(0);
		RPM = Input.GetMouseButton(1);
	}

	public void StartGame()
	{
		foreach(var tree in SpawnedTrees)
		{
			if(tree != null )
			{
				DestroyImmediate(tree);
			}
		}
		foreach(var tree in TreeSpawnPoints)
		{
			if(tree != null )
			{
				DestroyImmediate(tree);
			}
		}
		TreeSpawnPoints.Clear();
		SpawnedTrees.Clear();

		if(GameplayObject != null)
			DestroyImmediate(GameplayObject);

		GameplayObject = Instantiate<GameObject>(gameplayPrefab);
		GameplayObject.transform.SetParent(this.transform);

		hudObject.SetActive(true);
		mainMenuObject.SetActive(false);
		GameplayObject.SetActive(true);
		gameOverObject.SetActive(false);

		MainCharacter = GetComponentInChildren<ControllableCharacter>();
		PlayableArea = GetComponentInChildren<BG>().GetComponent<Collider2D>();

		Points = 0;
		PointsLabel.text = Points.ToString();

		MusicController.Instance.Play();

		CurrentGamePhase = EGamePhase.InProgress;
	}

	public void EndGame()
	{
		hudObject.SetActive(false);
		mainMenuObject.SetActive(false);
		//gameplayObject.SetActive(false);
		gameOverObject.SetActive(true);

		LostLabel.text = string.Format("You've managed to chop down {0} cybertree minions.", Points.ToString());

		CurrentGamePhase = EGamePhase.GameOver;
	}

	public void Quit()
	{
		hudObject.SetActive(false);
		mainMenuObject.SetActive(true);
		GameplayObject.SetActive(false);
		gameOverObject.SetActive(false);
		
		CurrentGamePhase = EGamePhase.Menu;
	}
}
