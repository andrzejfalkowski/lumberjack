using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RedBlink : MonoBehaviour
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
				Color color = myRenderer.material.GetVector("_Color");
				color.g = color.b = timer/BlinkTime;
				myRenderer.material.SetVector("_Color", color);
			}

			if(myImage != null)
			{
				Color color = myImage.color;
				color.g = color.b = timer/BlinkTime;
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
				Color color = myRenderer.material.GetVector("_Color");
				color.g = color.b = 1f - timer/BlinkTime;
				myRenderer.material.SetVector("_Color", color);
			}

			if(myImage != null)
			{
				Color color = myImage.color;
				color.g = color.b = 1f - timer/BlinkTime;
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
