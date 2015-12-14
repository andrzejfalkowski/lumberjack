using UnityEngine;
using System.Collections;

public class BlockRotation : MonoBehaviour 
{
	void LateUpdate () 
	{
		this.transform.eulerAngles = Vector3.zero;
	}
}
