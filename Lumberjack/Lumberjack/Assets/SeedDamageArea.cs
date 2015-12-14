using UnityEngine;
using System.Collections;

public class SeedDamageArea : MonoBehaviour 
{
	public Bomb ParentSeed;
	
	void OnTriggerEnter2D(Collider2D collider2d)
	{
		ControllableCharacter character = collider2d.GetComponent<ControllableCharacter>();
		if(character != null && !character.CollidingSeeds.Contains(ParentSeed))
			character.CollidingSeeds.Add(ParentSeed);
	}
	
	void OnTriggerExit2D(Collider2D collider2d)
	{
		ControllableCharacter character = collider2d.GetComponent<ControllableCharacter>();
		if(character != null && character.CollidingSeeds.Contains(ParentSeed))
			character.CollidingSeeds.Remove(ParentSeed);
	}
}
