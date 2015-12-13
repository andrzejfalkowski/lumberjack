using UnityEngine;
using System.Collections;

public class ZOrder : MonoBehaviour 
{
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		spriteRenderer.sortingOrder = (int)((10f - this.transform.position.y) * 1000f);
	}
}
