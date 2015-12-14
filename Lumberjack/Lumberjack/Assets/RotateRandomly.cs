using UnityEngine;
using System.Collections;

public class RotateRandomly : MonoBehaviour 
{
	private float amount;
	// Use this for initialization
	void Start () 
	{
		amount = Random.Range (-5f, 5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 angles = this.transform.localEulerAngles;
		angles.z += amount;
		this.transform.localEulerAngles = angles;
	}
}
