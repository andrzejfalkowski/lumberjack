using UnityEngine;
using System.Collections;

public class EnemyTree : MonoBehaviour 
{
	public int HP = 2;
	public EnemyTreeAnimation MyAnimation;
	public EnemyTreeMouseCollider MyMouseCollider;
	public BombSpawner MyBombSpawner;
	public RedBlink MyBlink;

	public GameObject DamagePrefab;
	public GameObject DiePrefab;
	void Start () 
	{
		if(MyAnimation != null)
			MyAnimation.PlayGrowAnimation();
	}
	
	public void PlayGrowAnimation()
	{
		MyAnimation.PlayGrowAnimation();
	}
	
	public void PlayIdleAnimation()
	{
		MyAnimation.PlayIdleAnimation();
	}

	public void DecreaseHP()
	{
		HP--;

		GameObject spawnedObject = Instantiate<GameObject>(DamagePrefab);
		ControllableCharacter character = GameController.Instance.MainCharacter;
		spawnedObject.transform.position = character.DamagePoints[(int)character.CurrentDirection].transform.position;
		spawnedObject.transform.SetParent(GameController.Instance.GameplayObject.transform);

		MyBlink.Blink();

		if(HP <= 0)
		{
			if(GameController.Instance.SpawnedTrees.Contains(this))
				GameController.Instance.SpawnedTrees.Remove(this);
			if(GameController.Instance.MainCharacter.CollidingTrees.Contains(this))
				GameController.Instance.MainCharacter.CollidingTrees.Remove(this);

			spawnedObject = Instantiate<GameObject>(DiePrefab);
			spawnedObject.transform.SetParent(GameController.Instance.GameplayObject.transform);
			spawnedObject.transform.position = this.transform.position;
			spawnedObject.transform.localScale = this.transform.localScale;

			GameController.Instance.Points++;
			GameController.Instance.PointsLabel.text = GameController.Instance.Points.ToString();

			GameController.Instance.SelfieMeter =  Mathf.Min(100f, GameController.Instance.SelfieMeter + 10f);

			Destroy(this.gameObject);
		}
	}
}
