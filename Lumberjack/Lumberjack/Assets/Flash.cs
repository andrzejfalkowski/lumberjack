using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flash : MonoBehaviour 
{
	enum EBlinkPhase
	{
		None,
		BlinkIn,
		BlinkOut
	}

	private Image myImage;
	public float BlinkTime = 0.1f;
	private float timer = 0f;
	private EBlinkPhase currentBlinkPhase = EBlinkPhase.None;
	void Awake() 
	{
		if(myImage == null)
			myImage = GetComponent<Image>();
	}
	
	public void Update()
	{
		if(currentBlinkPhase == EBlinkPhase.BlinkIn)
		{
			timer -= Time.deltaTime;
			
			if(myImage != null)
			{
				Color color = myImage.color;
				color.a = 1f - timer/BlinkTime;
				myImage.color = color;
			}
			
			if(timer <= 0f)
			{
				currentBlinkPhase = EBlinkPhase.BlinkOut;
				timer = BlinkTime;
			}
		}
		else if(currentBlinkPhase == EBlinkPhase.BlinkOut)
		{
			timer -= Time.deltaTime;
			
			if(myImage != null)
			{
				Color color = myImage.color;
				color.a = timer/BlinkTime;
				myImage.color = color;
			}
			
			if(timer <= 0f)
			{
				currentBlinkPhase = EBlinkPhase.None;
			}
		}
	}
	
	public void Blink()
	{
		currentBlinkPhase = EBlinkPhase.BlinkIn;
		timer = BlinkTime;
	}
}
