using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillCount : MonoBehaviour 
{
	public Text MyText;
	public Text MySecondaryText;
	float timer = 3f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void Show(string text, string text2)
	{
		MyText.color = Color.white;
		MySecondaryText.color = Color.white;
		MyText.text = text;
		MySecondaryText.text = text2;
		timer = 3f;
	}

	void Update()
	{
		if(timer > 0f)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			Color color = MyText.color;
			color.a -= Time.deltaTime;
			MyText.color = color;
			MySecondaryText.color = color;
		}
	}
}
