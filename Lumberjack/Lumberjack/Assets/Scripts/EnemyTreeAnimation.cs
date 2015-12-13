using UnityEngine;
using System.Collections;

public class EnemyTreeAnimation : MonoBehaviour 
{
	private Animator myAnimation;
	// Use this for initialization
	void Awake() 
	{
		if(myAnimation == null)
			myAnimation = GetComponent<Animator>();
	}
	
	public void PlayGrowAnimation()
	{
		myAnimation.Play("plant_grow");
	}
	
	public void PlayIdleAnimation()
	{
		myAnimation.Play("plant_idle");
	}
}
