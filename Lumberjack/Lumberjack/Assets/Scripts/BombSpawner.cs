using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour 
{
	public GameObject BombPrefab;
	public GameObject BombSpawnPrefab;

	private float cooldown = 2f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		cooldown -= Time.deltaTime;
		if(cooldown < 0)
		{

			Vector2 targetPoint = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 3f));
			while(!GameController.Instance.PlayableArea.OverlapPoint(targetPoint))
			{
				targetPoint = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 3f));
			}

			GameObject bombObject = Instantiate<GameObject>(BombPrefab);
			bombObject.transform.position = this.transform.position;
			Bomb bomb = bombObject.GetComponent<Bomb>();

			bomb.Destination = targetPoint;
			bomb.BombSpawnPrefab = BombSpawnPrefab;

			cooldown = Random.Range (1f, 2f);
		}
	}
}
