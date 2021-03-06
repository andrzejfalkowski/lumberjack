﻿using UnityEngine;
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
	[SerializeField]
	GameObject tutorialObject;

	public Image HealthBar;
	public Image HealthBarBG;

	public Image SelfieBar;
	public Image SelfieBarBG;

	public Flash FlashScript;
	public Blink SelfieIconBlink;

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
			{
				MyKillCount.Show("FIRST SAP!", "1 KILL");
				SoundsController.Instance.PlayAnnouncement(0);
				AchievementController.Instance.UnlockAchievement(EAchiementType.NoQuestionsAxed);
			}
			else if(points == 5)
			{
				MyKillCount.Show("MEDIOAKRE!", "5 KILLS");
				SoundsController.Instance.PlayAnnouncement(1);
			}
			else if(points == 10)
			{
				MyKillCount.Show("CHOPPING SPREE!", "10 KILLS");
				SoundsController.Instance.PlayAnnouncement(2);
			}
			else if(points == 20)
			{
				MyKillCount.Show("PLANTASTIC!", "20 KILLS");
				SoundsController.Instance.PlayAnnouncement(3);
			}
			else if(points == 30)
			{
				MyKillCount.Show("AXE EFFECT!", "30 KILLS");
				SoundsController.Instance.PlayAnnouncement(4);
			}
			else if(points == 40)
			{
				MyKillCount.Show("KILLING SPRUCE!", "40 KILLS");
				SoundsController.Instance.PlayAnnouncement(5);
			}
			else if(points == 50)
			{
				MyKillCount.Show("TREEMENDOUS!", "50 KILLS");
				SoundsController.Instance.PlayAnnouncement(6);
			}
			else if(points == 60)
			{
				MyKillCount.Show("EXTREEMINATOR!", "60 KILLS");
				SoundsController.Instance.PlayAnnouncement(7);
			}
			else if(points == 70)
			{
				MyKillCount.Show("TREERRIFIC!", "70 KILLS");
				SoundsController.Instance.PlayAnnouncement(8);
			}
			else if(points == 80)
			{
				MyKillCount.Show("UNSTUMPABLE!", "80 KILLS");
				SoundsController.Instance.PlayAnnouncement(9);
			}
			else if(points == 90)
			{
				MyKillCount.Show("TREEDICULOUS!", "90 KILLS");
				SoundsController.Instance.PlayAnnouncement(10);
			}
			else if(points == 100)
			{
				MyKillCount.Show("LEAF NO TREE ALIVE!", "100 KILLS");
				SoundsController.Instance.PlayAnnouncement(11);
				AchievementController.Instance.UnlockAchievement(EAchiementType.ThisGameIsGrowing);
			}
			else if(points == 125)
			{
				MyKillCount.Show("WOODICROUS!", "125 KILLS");
				SoundsController.Instance.PlayAnnouncement(12);
			}
			else if(points == 150)
			{
				MyKillCount.Show("RUN, FOREST, RUN!", "150 KILLS");
				SoundsController.Instance.PlayAnnouncement(13);
			}
			else if(points == 175)
			{
				MyKillCount.Show("PODLIKE!", "175 KILLS");
				SoundsController.Instance.PlayAnnouncement(14);
			}
			else if(points == 200)
			{
				MyKillCount.Show("WE'VE RUN OUT OF PUNS!", "200 KILLS");
				SoundsController.Instance.PlayAnnouncement(15);
				AchievementController.Instance.UnlockAchievement(EAchiementType.NoMorePunder);
			}
			else if(points == 250)
			{
				MyKillCount.Show("PAPER INDUSTRY!", "150 KILLS");
				SoundsController.Instance.PlayAnnouncement(16);
			}
			else if(points == 300)
			{
				MyKillCount.Show("OZON HOLE!", "200 KILLS");
				SoundsController.Instance.PlayAnnouncement(17);
			}
			else if(points == 500)
			{
				MyKillCount.Show("GLOBAL WARMING!", "500 KILLS");
				SoundsController.Instance.PlayAnnouncement(18);
				AchievementController.Instance.UnlockAchievement(EAchiementType.AllIGot);
			}
			else if(points == 1000)
			{
				AchievementController.Instance.UnlockAchievement(EAchiementType.Tunguska);
			}
		}
	}

	public float SelfieMeter;

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

		if(Input.GetKeyDown(KeyCode.Escape) && CurrentGamePhase == EGamePhase.InProgress)
			Quit();

		if(CurrentGamePhase == EGamePhase.InProgress && SelfieMeter < 100f)
		{
			SelfieMeter -= Time.deltaTime;
		}
		SelfieBar.fillAmount = SelfieMeter/100f;

		if( SelfieMeter >= 100f && CurrentGamePhase == EGamePhase.InProgress)
		{
			if(RPM)
			{
				MainCharacter.TakeSelfie();
				SelfieMeter = 0f;
			}
		}
	}


	public void TrySelfie()
	{
		if( SelfieMeter >= 100f && CurrentGamePhase == EGamePhase.InProgress)
		{
				MainCharacter.TakeSelfie();
				SelfieMeter = 0f;
		}
	}

	public void Tutorial()
	{
		StartGame();
		tutorialObject.SetActive(true);
		Time.timeScale = 0f;
	}

	public void CloseTutorial()
	{
		Time.timeScale = 1f;
		tutorialObject.SetActive(false);
	}

	public void StartGame()
	{
		Time.timeScale = 1f;

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

		SelfieIconBlink.StopBlink();

		Points = 0;
		PointsLabel.text = Points.ToString();

		SelfieMeter = 0f;

		MusicController.Instance.PlayGamePlay();

		CurrentGamePhase = EGamePhase.InProgress;
	}

	public void EndGame()
	{
		hudObject.SetActive(false);
		mainMenuObject.SetActive(false);
		//gameplayObject.SetActive(false);
		gameOverObject.SetActive(true);

		MusicController.Instance.PLayMenu();

		LostLabel.text = string.Format("You've managed to chop down {0} cybertree minions.", Points.ToString());

		CurrentGamePhase = EGamePhase.GameOver;
	}

	public void Quit()
	{
		hudObject.SetActive(false);
		mainMenuObject.SetActive(true);
		GameplayObject.SetActive(false);
		gameOverObject.SetActive(false);

		MusicController.Instance.PLayMenu();

		CurrentGamePhase = EGamePhase.Menu;
	}
}
