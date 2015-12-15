using UnityEngine;
using System.Collections;

public class NewBomb : MonoBehaviour 
{
	public bool Seed = false;

	public float MovementSpeed = 5f;
	public float Height = 20f;
	public float Damage = 1f;
	private float startDistance = 0f;

	public SpriteRenderer BombGraphic;
	//private SpriteRenderer shadowGraphic;
	public Vector3 UpDestination = Vector3.zero;
	public Vector3 Destination;

	public GameObject BombSpawnPrefab;
	public GameObject ExplosionPrefab;
	public Collider2D ExplosionCollider;

	public GameObject TreeSpawnPointPrefab;

	EPhase CurrentPhase = EPhase.MoveUp;
	enum EPhase
	{
		MoveUp,
		MoveDown
	}

	// Use this for initialization
	void Start () 
	{
		startDistance = (this.transform.position - Destination).magnitude;
		//shadowGraphic = this.GetComponent<SpriteRenderer>();

		if(UpDestination == Vector3.zero)
		{
			UpDestination = this.transform.position;
			UpDestination.y += Height;
		}
	}

//	void OnCollisionEnter2D(Collision2D collision)
//	{
//		ControllableCharacter character = collision.collider.GetComponent<ControllableCharacter>();
//		if(character != null && Damage > 0f)
//		{
//			character.DecreaseHP(Damage);
//			Destroy(this.gameObject);
//		}
//	}

	void Update()
	{
		Vector3 pos = this.transform.position;
		if(CurrentPhase == EPhase.MoveUp)
		{
			Vector3 moveDirection = pos - UpDestination;
			pos.x -= moveDirection.x * 0.1f * Time.deltaTime * MovementSpeed;
			pos.y += Time.deltaTime * MovementSpeed * 3f;
			this.transform.position = pos;
			if(moveDirection.y > 0.1f)
			{
				BombGraphic.transform.position = Destination;
				Color color = BombGraphic.color;
				color.a = 0f;
				BombGraphic.color = color;

				CurrentPhase = EPhase.MoveDown;
				Vector3 newPos = Destination;
				newPos.y += Height;
				this.transform.position = newPos;
			}
		}
		else if(CurrentPhase == EPhase.MoveDown)
		{
			Vector3 moveDirection = pos - Destination;
			//pos.x -= Mathf.Sign(moveDirection.x) * Time.deltaTime * MovementSpeed;
			pos.y -= Time.deltaTime * MovementSpeed;
			this.transform.position = pos;

			BombGraphic.transform.position = Destination;
			Color color = BombGraphic.color;
			color.a = (1f - (pos.y - Destination.y)/Height) * 0.5f;
			BombGraphic.color = color;

			Vector3 scale = new Vector3(1f + 4f * ((pos.y - Destination.y)/Height), 1f + 4f * ((pos.y - Destination.y)/Height), 1f);
			BombGraphic.transform.localScale = scale;

			if(moveDirection.y < 0.1f)
			{
				Explode();
			}
		}
	}

	void Explode()
	{
		Vector3 pos = this.transform.position;
		if(BombSpawnPrefab != null)
		{
			bool blocked = false;
			foreach(var tree in GameController.Instance.SpawnedTrees)
			{
				if(tree != null && tree.MyDamageCollider.OverlapPoint(this.transform.position))
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

				EnemyTree tree = spawnedObject.GetComponent<EnemyTree>();
				if(tree != null)
				{
					tree.HP = Random.Range(1, 4);
					GameController.Instance.SpawnedTrees.Add(tree);
				}

			}
		}
		if(ExplosionPrefab != null)
		{
			GameObject spawnedObject = Instantiate<GameObject>(ExplosionPrefab);
			spawnedObject.transform.position = this.transform.position;
			spawnedObject.transform.SetParent(GameController.Instance.GameplayObject.transform);
		}
		if(Damage > 0f)
		{
			ControllableCharacter character = GameController.Instance.MainCharacter;
			if(ExplosionCollider.OverlapPoint(character.transform.position))
			{
				character.DecreaseHP(Damage);
			}
		}

		AudioSource BombSfx = GetComponent<AudioSource>();
		if(BombSfx != null)
		{
			AudioSource newBombSfx = SoundsController.Instance.gameObject.AddComponent<AudioSource>();
			newBombSfx.clip = BombSfx.clip;
			newBombSfx.Play();
			Destroy(newBombSfx, 2f);
		}

		DestroyImmediate(this.gameObject);
	}

//	void Update () 
//	{	
//		Vector3 pos = this.transform.localPosition;
//		if(Mathf.Abs(Destination.x - pos.x) < 0.1f && 
//		   Mathf.Abs(Destination.y - pos.y) < 0.1f)
//		{

//		}
//		else
//		{
//			Vector3 moveDirection = pos - Destination;
//
//			Vector3 graphicPos = BombGraphic.transform.localPosition;
//			graphicPos.y = Height - Height * Mathf.Pow(1f - moveDirection.magnitude / startDistance, 2f);
//			BombGraphic.transform.localPosition = graphicPos;
//
//			moveDirection.Scale(new Vector3(10f, 10f, 1f));
//			moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
//			
//			pos.x = pos.x - moveDirection.x * Time.deltaTime * MovementSpeed;
//			pos.y = pos.y - moveDirection.y * Time.deltaTime * MovementSpeed;
//			this.transform.localPosition = pos;
//
//		}
//	}
}
