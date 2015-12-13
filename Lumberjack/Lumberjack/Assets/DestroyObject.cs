using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour 
{
	public void Destroy()
	{
		Destroy(this.gameObject);
	}
}
