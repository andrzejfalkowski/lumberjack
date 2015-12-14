using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		Vector3 scale = this.transform.localScale;
		scale.x += Random.Range(-0.3f, 0.3f);
		scale.y += Random.Range(-0.3f, 0.3f);
		this.transform.localScale = scale;
	}

}
