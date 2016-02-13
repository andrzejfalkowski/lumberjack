using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum EAchiementType
{
	NoQuestionsAxed,	
	ThisGameIsGrowing,	
	LumberFab,
	NoMorePunder,
	CuttingEdge,
	Photobomb,
	OneTwoTree,
	LumberSlumber,
	AllIGot,
	Tunguska
}

public class AchievementController : MonoBehaviour 
{
	private static AchievementController instance = null;
	public static AchievementController Instance
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

	public Dictionary<EAchiementType, int> Trophies = new Dictionary<EAchiementType, int>()
	{
		{EAchiementType.NoQuestionsAxed, 50572},
		{EAchiementType.ThisGameIsGrowing, 50573},
		{EAchiementType.LumberFab, 50577},
		{EAchiementType.NoMorePunder, 50574},
		{EAchiementType.CuttingEdge, 50578},
		{EAchiementType.Photobomb, 50579},
		{EAchiementType.OneTwoTree, 50580},
		{EAchiementType.LumberSlumber, 50581},
		{EAchiementType.AllIGot, 50575},
		{EAchiementType.Tunguska, 50576},
	};
	
	public void Init () 
	{
//		Debug.Log (GameJolt.API.Manager.Instance.CurrentUser == null);
//		if(GameJolt.API.Manager.Instance.CurrentUser == null)
//		{
//			GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
//				if (success)
//				{
//					Debug.Log("The user signed in!");
//					GetAchievements();
//				}
//				else
//				{
//					Debug.Log("The user failed to signed in or closed the window :(");
//					}
//				});
//		}
//		else
//		{
//			GetAchievements();
//		}


	}

	public void AchievementsClicked()
	{
		Debug.Log(GameJolt.API.Manager.Instance.CurrentUser == null);
		if(GameJolt.API.Manager.Instance.CurrentUser == null)
		{
			GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
				if (success)
				{
					Debug.Log("The user signed in!");
					GetAchievements(()=>ShowAchievements());
				}
				else
				{
					Debug.Log("The user failed to signed in or closed the window :(");
				}
			});
		}
		else
		{
			ShowAchievements();
		}
	}

	public void ShowAchievements()
	{
		GameJolt.UI.Manager.Instance.ShowTrophies();
	}

	public void GetAchievements(Action callback = null)
	{
		GameJolt.API.Trophies.Get((GameJolt.API.Objects.Trophy[] trophies) => {
			if (trophies != null)
			{
				foreach (var trophy in trophies)//.Reverse<GameJolt.API.Objects.Trophy>())
				{
					Debug.Log(string.Format("> {0} - {1} - {2} - {3}Unlocked", trophy.Title, trophy.ID, trophy.Difficulty, trophy.Unlocked ? "" : "Not "));
				}
				Debug.Log(string.Format("Found {0} trophies.", trophies.Length));
				if(callback != null)
					callback();
			}
		});
	}

	public void UnlockAchievement(EAchiementType trophyType)
	{
		if(!Trophies.ContainsKey(trophyType))
			return;

		Debug.Log ("UnlockAchievement " + trophyType);

		int trophyID = Trophies[trophyType];

		GameJolt.API.Trophies.Unlock(trophyID, (bool success) => {
			if (success)
			{
				Debug.Log("Success!");
			}
			else
			{
				Debug.Log("Something went wrong");
			}
		});
	}
}
