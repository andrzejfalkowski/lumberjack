using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour 
{
	public GameObject BombPrefab;
	public GameObject BombSpawnPrefab;
	
	public float MinCooldownStart = 3f;
	public float MaxCooldownStart  = 8f;
	public float MinCooldownFinal = 1f;
	public float MaxCooldownFinal  = 2f;
	[SerializeField]
	private float minCooldown;
	[SerializeField]
	private float maxCooldown;

	public float DifficultyTimer = 30f;
	private float diffTimer;
	bool dynamicDiff = true;

	private float cooldown = 2f;

	public GameObject TrajectoryPoint;

	// Use this for initialization
	void Start () 
	{
		minCooldown = MinCooldownStart;
		maxCooldown = MaxCooldownStart;
		diffTimer = DifficultyTimer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(dynamicDiff)
		{
			diffTimer -= Time.deltaTime;
			minCooldown = MinCooldownFinal + (diffTimer/DifficultyTimer) * (MinCooldownStart - MinCooldownFinal);
			maxCooldown = MaxCooldownFinal + (diffTimer/DifficultyTimer) * (MaxCooldownStart - MaxCooldownFinal);
			if(diffTimer <= 0f)
				dynamicDiff = false;
		}

		cooldown -= Time.deltaTime;
		if(cooldown < 0)
		{
			BG background = GameController.Instance.PlayableArea.GetComponent<BG>();
			Vector2 targetPoint = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 3f));
			int cnt = 0;
			bool blocked = false;
			if(BombPrefab.GetComponent<Bomb>() != null)
			{
				//Debug.Log (background.BlockColliders.Count);
				foreach(var block in background.BlockColliders)
				{
					if(block.OverlapPoint(targetPoint))
					{
						//Debug.Log ("blocked!");
						blocked = true;
						break;
					}
				}
			}
			while(!GameController.Instance.PlayableArea.OverlapPoint(targetPoint) && blocked && cnt < 10)
			{
				blocked = false;
				if(BombPrefab.GetComponent<Bomb>() != null)
				{
					//Debug.Log (background.BlockColliders.Count);
					foreach(var block in background.BlockColliders)
					{
						if(block.OverlapPoint(targetPoint))
						{
							//Debug.Log ("blocked!");
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

			NewBomb newBomb = bombObject.GetComponent<NewBomb>();
			if(newBomb != null)
			{
				if(newBomb.Damage > 0f && Random.Range(0, 10) > 7)
				{
					targetPoint = GameController.Instance.MainCharacter.transform.localPosition;
					targetPoint.x += Random.Range (-0.1f, 0.1f);
					targetPoint.y += Random.Range (-0.1f, 0.1f);
				}

				newBomb.Destination = targetPoint;

				if(TrajectoryPoint != null)
					newBomb.UpDestination = TrajectoryPoint.transform.position;

				newBomb.BombSpawnPrefab = BombSpawnPrefab;
			}

			Bomb bomb = bombObject.GetComponent<Bomb>();
			if(bomb != null)
			{
				bomb.Destination = targetPoint;
				bomb.BombSpawnPrefab = BombSpawnPrefab;
			}

			SoundsController.Instance.PlayLauncherSound();

			cooldown = Random.Range (minCooldown, maxCooldown);
		}
	}
}
