using UnityEngine;
using System.Collections;

public class CameraFitter : MonoBehaviour 
{

	// Use this for initialization
	void Update () 
	{
		GetComponent<Camera>().orthographicSize = 17.8f * Screen.height / Screen.width * 0.5f;
	}

}
