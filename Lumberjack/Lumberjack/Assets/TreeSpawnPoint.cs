using UnityEngine;
using System.Collections;

public class TreeSpawnPoint : MonoBehaviour 
{
	public GameObject BombSpawnPrefab;

	float timer = 1f;
	
	void Update () 
	{
		timer -= Time.deltaTime;

		if(timer < 0f)
		{
			GameObject spawnedObject = Instantiate<GameObject>(BombSpawnPrefab);
			spawnedObject.transform.position = this.transform.position;
			spawnedObject.transform.SetParent(GameController.Instance.GameplayObject.transform);
			GameController.Instance.SpawnedTrees.Add(spawnedObject.GetComponent<EnemyTree>());

			Destroy(this.gameObject);
		}
	}
}
