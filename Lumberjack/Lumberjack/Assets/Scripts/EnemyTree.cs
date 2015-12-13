using UnityEngine;
using System.Collections;

public class EnemyTree : MonoBehaviour 
{
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
}
