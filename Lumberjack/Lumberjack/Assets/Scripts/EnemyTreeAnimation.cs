using UnityEngine;
using System.Collections;

public class EnemyTreeAnimation : MonoBehaviour 
{
	private Animator myAnimation;
	private SpriteRenderer myRenderer;
	// Use this for initialization
	void Awake() 
	{
		if(myAnimation == null)
			myAnimation = GetComponent<Animator>();

		if(myRenderer == null)
			myRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void PlayGrowAnimation()
	{
		myAnimation.Play("plant_grow");
	}
	
	public void PlayIdleAnimation()
	{
		myAnimation.Play("plant_idle");
	}

	public void Flash()
	{
		myRenderer.material.SetFloat("_FlashAmount", 0.15f); 
	}

	public void DeFlash()
	{
		myRenderer.material.SetFloat("_FlashAmount", 0f); 
	}
}
