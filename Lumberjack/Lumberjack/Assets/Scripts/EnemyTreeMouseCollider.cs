using UnityEngine;
using System.Collections;

public class EnemyTreeMouseCollider : MonoBehaviour 
{
	public EnemyTree ParentTree;
	
	bool clicked = false;
	bool inArea = false;
	
	void Update()
	{
		if(GameController.Instance.LPM && inArea)
		{
			GameController.Instance.MainCharacter.Attack(ParentTree);
		}
	}

	void OnMouseEnter()
	{
		inArea = true;
		ParentTree.MyAnimation.Flash();
	}
	
	void OnMouseExit()
	{
		inArea = false;
		ParentTree.MyAnimation.DeFlash();
	}
}
