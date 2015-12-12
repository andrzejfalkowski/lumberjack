using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ECharacterCondition
{
	Alive,
	Dead
}

public enum ECharacterState
{
	Idle,
	Moving,
	Attacking
}

public class ControllableCharacter : MonoBehaviour 
{
	public string Name = "Name";
	
	public float MovementSpeed = 1f;

	public ECharacterCondition CurrentCondition = ECharacterCondition.Alive;
	public ECharacterState CurrentState = ECharacterState.Idle;

	public Vector3 Destination;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(CurrentState == ECharacterState.Moving)
		{
			Vector3 pos = this.transform.localPosition;
			if(Mathf.Abs(Destination.x - pos.x) < 0.1f && 
			   Mathf.Abs(Destination.y - pos.y) < 0.1f)
			{
				//GameController.Instance.CharacterActionEnd(this);
			}
			else
			{
				Vector3 moveDirection = pos - Destination;
				moveDirection.Scale(new Vector3(10f, 10f, 1f));
				moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

				float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90f;
				this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				pos.x = pos.x - moveDirection.x * Time.deltaTime * MovementSpeed;
				pos.y = pos.y - moveDirection.y * Time.deltaTime * MovementSpeed;
				this.transform.localPosition = pos;
			}
		}
	}

	void OnMouseEnter()
	{
		//GameController.Instance.SetCursorHint("Select: " + Name);
	}

	void OnMouseDown()
	{
		//GameController.Instance.SelectHero(this);
	}

	public void GoToSpot(Vector3 target)
	{
		Destination = target;

		CurrentState = ECharacterState.Moving;
	}
}