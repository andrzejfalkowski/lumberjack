using UnityEngine;
using System.Collections;

public class EnemyTree : MonoBehaviour 
{
	public int HP = 2;
	public EnemyTreeAnimation MyAnimation;

	void Start () 
	{
		if(MyAnimation != null)
			MyAnimation.PlayGrowAnimation();
	}
	
	public void PlayGrowAnimation()
	{
		MyAnimation.PlayGrowAnimation();
	}
	
	public void PlayIdleAnimation()
	{
		MyAnimation.PlayIdleAnimation();
	}

	public void DecreaseHP()
	{
		HP--;
		if(HP <= 0)
		{
			if(GameController.Instance.SpawnedTrees.Contains(this))
				GameController.Instance.SpawnedTrees.Remove(this);
			if(GameController.Instance.MainCharacter.CollidingTrees.Contains(this))
				GameController.Instance.MainCharacter.CollidingTrees.Remove(this);
			Destroy(this.gameObject);
		}
	}
}
