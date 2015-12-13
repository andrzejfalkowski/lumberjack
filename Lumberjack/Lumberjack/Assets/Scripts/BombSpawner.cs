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
			BG background = GameController.Instance.PlayableArea.GetComponent<BG>();
			Vector2 targetPoint = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 3f));
			int cnt = 0;
			bool blocked = false;
			if(BombPrefab.GetComponent<Bomb>().Seed)
			{
				Debug.Log (background.BlockColliders.Count);
				foreach(var block in background.BlockColliders)
				{
					if(block.OverlapPoint(targetPoint))
					{
						Debug.Log ("blocked!");
						blocked = true;
						break;
					}
				}
			}
			while(!GameController.Instance.PlayableArea.OverlapPoint(targetPoint) && blocked && cnt < 10)
			{
				blocked = false;
				if(BombPrefab.GetComponent<Bomb>().Seed)
				{
					Debug.Log (background.BlockColliders.Count);
					foreach(var block in background.BlockColliders)
					{
						if(block.OverlapPoint(targetPoint))
						{
							Debug.Log ("blocked!");
							blocked = true;
							break;
						}
					}
				}
				targetPoint = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 3f));
				cnt++;
			}

			if(!GameController.Instance.PlayableArea.OverlapPoint(targetPoint) || blocked)
			   return;

			GameObject bombObject = Instantiate<GameObject>(BombPrefab);
			bombObject.transform.SetParent(GameController.Instance.GameplayObject.transform);
			bombObject.transform.position = this.transform.position;
			Bomb bomb = bombObject.GetComponent<Bomb>();

			bomb.Destination = targetPoint;
			bomb.BombSpawnPrefab = BombSpawnPrefab;

			cooldown = Random.Range (1f, 2f);
		}
	}
}
