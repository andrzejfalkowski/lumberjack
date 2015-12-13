using UnityEngine;
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
	const float BLINK_TIME = 0.1f;
	private float timer = 0f;
	private EBlinkPhase currentBlinkPhase = EBlinkPhase.None;
	void Awake() 
	{

		if(myRenderer == null)
			myRenderer = GetComponent<SpriteRenderer>();
	}

	public void Update()
	{
		if(currentBlinkPhase == EBlinkPhase.BlinkIn)
		{
			timer -= Time.deltaTime;
			Color color = myRenderer.material.GetVector("_Color");
			color.g = color.b = timer/BLINK_TIME;
			myRenderer.material.SetVector("_Color", color);
			if(timer <= 0f)
			{
				currentBlinkPhase = EBlinkPhase.BlinkOut;
				timer = BLINK_TIME;
			}
		}
		else if(currentBlinkPhase == EBlinkPhase.BlinkOut)
		{
			timer -= Time.deltaTime;
			Color color = myRenderer.material.GetVector("_Color");
			color.g = color.b = 1f - timer/BLINK_TIME;
			myRenderer.material.SetVector("_Color", color);
			if(timer <= 0f)
			{
				currentBlinkPhase = EBlinkPhase.None;
			}
		}
	}

	public void Blink()
	{
		currentBlinkPhase = EBlinkPhase.BlinkIn;
		timer = BLINK_TIME;
	}
}
