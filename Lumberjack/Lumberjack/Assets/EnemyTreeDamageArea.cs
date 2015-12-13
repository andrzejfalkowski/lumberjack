using UnityEngine;
using System.Collections;

public class EnemyTreeDamageArea : MonoBehaviour 
{
	public EnemyTree ParentTree;

	void OnTriggerEnter2D(Collider2D collider2d)
	{
		ControllableCharacter character = collider2d.GetComponent<ControllableCharacter>();
		if(character != null && !character.CollidingTrees.Contains(ParentTree))
			character.CollidingTrees.Add(ParentTree);
	}

	void OnTriggerExit2D(Collider2D collider2d)
	{
		ControllableCharacter character = collider2d.GetComponent<ControllableCharacter>();
		if(character != null && character.CollidingTrees.Contains(ParentTree))
			character.CollidingTrees.Remove(ParentTree);
	}
}
