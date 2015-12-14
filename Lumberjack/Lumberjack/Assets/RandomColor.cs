using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour 
{
	void Start () 
	{
		Color color = GetComponent<SpriteRenderer>().color;
		color.g -= Random.Range (0f, 0.1f);
		color.b -= Random.Range (0f, 0.2f);
		GetComponent<SpriteRenderer>().material.SetVector("_Color", color);
	}
}
