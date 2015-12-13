using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZOrder : MonoBehaviour 
{
	[SerializeField]
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
	[SerializeField]
	int modifier = 0;

	// Use this for initialization
	void Start () 
	{
		if(spriteRenderers.Count == 0)
			spriteRenderers.Add(GetComponent<SpriteRenderer>());
	}
	
	// Update is called once per frame
	void Update () 
	{
		int order = (int)((10f - this.transform.position.y) * 1000f) + modifier;
		int cnt = 0;
		foreach(var spriteRenderer in spriteRenderers)
		{
			spriteRenderer.sortingOrder = order + cnt;
			cnt++;
		}
	}
}
