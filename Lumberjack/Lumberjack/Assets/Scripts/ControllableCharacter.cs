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

public enum ECharacterDirection
{
	Left,
	Right
}

public class ControllableCharacter : MonoBehaviour 
{
	public string Name = "Name";
	
	public float MovementSpeed = 1f;

	public ECharacterCondition CurrentCondition = ECharacterCondition.Alive;
	public ECharacterState CurrentState = ECharacterState.Idle;
	public ECharacterDirection CurrentDirection = ECharacterDirection.Left;

	private Animator myAnimation;

	public Vector3 Destination;

	// Use this for initialization
	void Start () 
	{
		myAnimation = GetComponent<Animator>();
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
				CurrentState = ECharacterState.Idle;
			}
			else
			{
				Vector3 moveDirection = pos - Destination;
				moveDirection.Scale(new Vector3(10f, 10f, 1f));
				moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

				//float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90f;
				//this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				if(moveDirection.x > 0)
					CurrentDirection = ECharacterDirection.Right;
				else
					CurrentDirection = ECharacterDirection.Left;

				pos.x = pos.x - moveDirection.x * Time.deltaTime * MovementSpeed;
				pos.y = pos.y - moveDirection.y * Time.deltaTime * MovementSpeed;
				this.transform.localPosition = pos;
			}
			
			PlayMoveAnimation();
		}
		else if(CurrentState == ECharacterState.Idle)
		{
			PlayIdleAnimation();
		}

	}

	void PlayIdleAnimation()
	{
		switch(CurrentDirection)
		{
			case ECharacterDirection.Left:
				myAnimation.Play("idle_left");
			break;
			case ECharacterDirection.Right:
				myAnimation.Play("idle_right");
			break;
		}
	}

	void PlayMoveAnimation()
	{
		switch(CurrentDirection)
		{
		case ECharacterDirection.Left:
			myAnimation.Play("move_left");
			break;
		case ECharacterDirection.Right:
			myAnimation.Play("move_right");
			break;
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

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Collision!");
	}
}