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
	Right,
	Up,
	Down
}

public class ControllableCharacter : MonoBehaviour 
{
	public string Name = "Name";
	
	public float MovementSpeed = 1f;

	public ECharacterCondition CurrentCondition = ECharacterCondition.Alive;
	public ECharacterState CurrentState = ECharacterState.Idle;
	public ECharacterState PreviousState = ECharacterState.Idle;
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
		if(Input.GetMouseButtonDown(1) && CurrentState != ECharacterState.Attacking && PreviousState != ECharacterState.Attacking)
		{
			PreviousState = CurrentState;
			CurrentState = ECharacterState.Attacking;
			PlayAttackAnimation();
		}
		else
		{
			PreviousState = CurrentState;
		}

		if(CurrentState == ECharacterState.Moving)
		{
			Vector3 pos = this.transform.localPosition;
			if(Mathf.Abs(Destination.x - pos.x) < 0.1f && 
			   Mathf.Abs(Destination.y - pos.y) < 0.1f)
			{
				//GameController.Instance.CharacterActionEnd(this);
				PreviousState = CurrentState;
				CurrentState = ECharacterState.Idle;
			}
			else
			{
				Vector3 moveDirection = pos - Destination;
				moveDirection.Scale(new Vector3(10f, 10f, 1f));
				moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

				//float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90f;
				//this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				if(moveDirection.x < 0 && Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
					CurrentDirection = ECharacterDirection.Right;
				else if(moveDirection.x >= 0 && Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
					CurrentDirection = ECharacterDirection.Left;
				else if(moveDirection.y < 0)
					CurrentDirection = ECharacterDirection.Up;
				else if(moveDirection.y >= 0)
					CurrentDirection = ECharacterDirection.Down;

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
			case ECharacterDirection.Up:
				myAnimation.Play("idle_up");
			break;
			case ECharacterDirection.Down:
				myAnimation.Play("idle_down");
			break;
		}
	}

	void PlayAttackAnimation()
	{
		switch(CurrentDirection)
		{
		case ECharacterDirection.Left:
			myAnimation.Play("attack_left");
			break;
		case ECharacterDirection.Right:
			myAnimation.Play("attack_right");
			break;
		case ECharacterDirection.Up:
			myAnimation.Play("attack_up");
			break;
		case ECharacterDirection.Down:
			myAnimation.Play("attack_down");
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
		case ECharacterDirection.Up:
			myAnimation.Play("move_up");
			break;
		case ECharacterDirection.Down:
			myAnimation.Play("move_down");
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
		if(CurrentState == ECharacterState.Attacking)
			return;

		Destination = target;

		PreviousState = CurrentState;
		CurrentState = ECharacterState.Moving;
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Collision!");
	}

	public void HandleAttackFinished()
	{
		PreviousState = CurrentState;
		CurrentState = ECharacterState.Idle;
		PlayIdleAnimation();
	}
}