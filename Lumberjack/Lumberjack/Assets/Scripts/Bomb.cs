using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
	public bool Seed = false;

	public float MovementSpeed = 5f;
	public float Height = 1.1f;
	public float Damage = 1f;
	private float startDistance = 0f;

	public EnemyTreeAnimation MyAnimation;
	public SpriteRenderer BombGraphic;
	private SpriteRenderer shadowGraphic;
	public Vector3 Destination;

	public GameObject BombSpawnPrefab;
	public GameObject ExplosionPrefab;

	public GameObject TreeSpawnPointPrefab;

	// Use this for initialization
	void Start () 
	{
		startDistance = (this.transform.localPosition - Destination).magnitude;
		shadowGraphic = this.GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		ControllableCharacter character = collision.collider.GetComponent<ControllableCharacter>();
		if(character != null && Damage > 0f)
		{
			character.DecreaseHP(Damage);
			Destroy(this.gameObject);
		}
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
					if(tree != null && tree.GetComponent<Collider2D>().OverlapPoint(this.transform.position))
					{
						blocked = true;
						break;
					}
				}
				foreach(var tree in GameController.Instance.TreeSpawnPoints)
				{
					if(tree != null && tree.GetComponent<Collider2D>().OverlapPoint(this.transform.position))
					{
						blocked = true;
						break;
					}
				}
				if(!blocked)
				{
					GameObject spawnedObject = Instantiate<GameObject>(TreeSpawnPointPrefab);
					spawnedObject.transform.position = pos;
					spawnedObject.transform.SetParent(GameController.Instance.GameplayObject.transform);

					spawnedObject.GetComponent<TreeSpawnPoint>().BombSpawnPrefab = BombSpawnPrefab;

					GameController.Instance.SpawnedTrees.Add(spawnedObject.GetComponent<EnemyTree>());
				}
			}
			if(ExplosionPrefab != null)
			{
				GameObject spawnedObject = Instantiate<GameObject>(ExplosionPrefab);
				spawnedObject.transform.position = this.transform.position;
				spawnedObject.transform.SetParent(GameController.Instance.GameplayObject.transform);
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
		}
	}

	public void DecreaseHP()
	{		
		if(GameController.Instance.MainCharacter.CollidingSeeds.Contains(this))
			GameController.Instance.MainCharacter.CollidingSeeds.Remove(this);
		if(this != null && this.gameObject != null)
		{
			GameController.Instance.Points++;
			GameController.Instance.PointsLabel.text = GameController.Instance.Points.ToString();

			Destroy(this.gameObject);
		}
	}
}
