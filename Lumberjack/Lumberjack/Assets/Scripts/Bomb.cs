using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
	public float MovementSpeed = 5f;
	public float Height = 1.1f;
	private float startDistance = 0f;

	public SpriteRenderer BombGraphic;
	private SpriteRenderer shadowGraphic;
	public Vector3 Destination;

	public GameObject BombSpawnPrefab;
	public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () 
	{
		startDistance = (this.transform.localPosition - Destination).magnitude;
		shadowGraphic = this.GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{	
		Vector3 pos = this.transform.localPosition;
		if(Mathf.Abs(Destination.x - pos.x) < 0.1f && 
		   Mathf.Abs(Destination.y - pos.y) < 0.1f)
		{
			if(BombSpawnPrefab != null)
			{
				bool blocked = false;
				foreach(var tree in GameController.Instance.SpawnedTrees)
				{
					if(tree.GetComponent<Collider2D>().OverlapPoint(this.transform.position))
					{
						blocked = true;
						break;
					}
				}
				if(!blocked)
				{
					GameObject spawnedObject = Instantiate<GameObject>(BombSpawnPrefab);
					spawnedObject.transform.position = this.transform.position;
					GameController.Instance.SpawnedTrees.Add(spawnedObject.GetComponent<EnemyTree>());
				}
			}
			if(ExplosionPrefab != null)
			{
				GameObject spawnedObject = Instantiate<GameObject>(ExplosionPrefab);
				spawnedObject.transform.position = this.transform.position;
			}
			DestroyImmediate(this.gameObject);
		}
		else
		{
			Vector3 moveDirection = pos - Destination;

			Vector3 graphicPos = BombGraphic.transform.localPosition;
			graphicPos.y = Height - Height * Mathf.Pow(1f - moveDirection.magnitude / startDistance, 2f);
			BombGraphic.transform.localPosition = graphicPos;

			moveDirection.Scale(new Vector3(10f, 10f, 1f));
			moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
			
			pos.x = pos.x - moveDirection.x * Time.deltaTime * MovementSpeed;
			pos.y = pos.y - moveDirection.y * Time.deltaTime * MovementSpeed;
			this.transform.localPosition = pos;

//			int sortingOrder = GameController.Instance.MainCharacter.GetComponent<SpriteRenderer>().sortingOrder;
//			if(GameController.Instance.MainCharacter.transform.position.y < this.transform.position.y)
//			{
//				shadowGraphic.sortingOrder = sortingOrder - 1;
//				BombGraphic.sortingOrder = sortingOrder - 1;
//			}
//			else
//			{
//				shadowGraphic.sortingOrder = sortingOrder + 1;
//				BombGraphic.sortingOrder = sortingOrder + 1;
//			}
		}
	}
}
