using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour
{
	enum EBlinkPhase
	{
		None,
		BlinkIn,
		BlinkOut
	}
	
	private SpriteRenderer myRenderer;
	private Image myImage;
	public float BlinkTime = 0.1f;
	private float timer = 0f;
	private EBlinkPhase currentBlinkPhase = EBlinkPhase.None;
	void Awake() 
	{
		if(myRenderer == null)
			myRenderer = GetComponent<SpriteRenderer>();
		if(myImage == null)
			myImage = GetComponent<Image>();
	}
	
	public void Update()
	{
		if(currentBlinkPhase == EBlinkPhase.BlinkIn)
		{
			timer -= Time.deltaTime;
			
			if(myRenderer != null)
			{
				float flash = timer/BlinkTime;
				myRenderer.material.SetFloat("_FlashAmount", flash * 0.5f);
			}
			
			if(myImage != null)
			{
				Color color = myImage.color;
				color.a = (timer/BlinkTime) * 0.5f;
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
			
			if(myRenderer != null)
			{
				float flash = 1f - timer/BlinkTime;
				myRenderer.material.SetFloat("_FlashAmount", flash * 0.5f);
			}
			
			if(myImage != null)
			{
				Color color = myImage.color;
				color.a = (1f - timer/BlinkTime) * 0.5f;
				myImage.color = color;
			}
			
			if(timer <= 0f)
			{
				currentBlinkPhase = EBlinkPhase.BlinkIn;
				timer = BlinkTime;
			}
		}
	}
	
	public void StartBlink()
	{
		currentBlinkPhase = EBlinkPhase.BlinkIn;
		timer = BlinkTime;
	}

	public void StopBlink()
	{
		currentBlinkPhase = EBlinkPhase.None;
		if(myRenderer != null)
		{
			myRenderer.material.SetFloat("_FlashAmount", 0f);
		}
		if(myImage != null)
		{
			Color color = myImage.color;
			color.a = 0f;
			myImage.color = color;
		}
	}
}
