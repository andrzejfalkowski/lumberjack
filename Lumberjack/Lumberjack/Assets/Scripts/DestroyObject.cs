using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour 
{
	public GameObject ObjectToDestroy;
	public void Destroy()
	{
		if(ObjectToDestroy == null)
			Destroy(this.gameObject);
		else
			Destroy(ObjectToDestroy);
	}
}
