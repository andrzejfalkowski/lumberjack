using UnityEngine;
using System.Collections;

public class EnemyTreeMouseCollider : MonoBehaviour 
{
	public EnemyTree ParentTree;
	
	bool clicked = false;
	bool inArea = false;
	
	void Update()
	{
		if(clicked && inArea)
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
	
	void OnMouseDown()
	{
		clicked = true;
	}
	
	void OnMouseUp()
	{
		clicked = false;
	}
}
