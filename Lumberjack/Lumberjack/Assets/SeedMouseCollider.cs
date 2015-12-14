using UnityEngine;
using System.Collections;

public class SeedMouseCollider : MonoBehaviour 
{
	public Bomb ParentSeed;
	
	bool clicked = false;
	bool inArea = false;
	
	void Update()
	{
		if(GameController.Instance.LPM && inArea)
		{
			GameController.Instance.MainCharacter.AttackSeed(ParentSeed);
		}
	}
	
	void OnMouseEnter()
	{
		inArea = true;
		ParentSeed.MyAnimation.Flash();
	}
	
	void OnMouseExit()
	{
		inArea = false;
		ParentSeed.MyAnimation.DeFlash();
	}
}
