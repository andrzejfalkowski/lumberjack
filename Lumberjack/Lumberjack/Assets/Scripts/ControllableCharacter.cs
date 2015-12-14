﻿using UnityEngine;
using UnityEngine.UI;
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
	Attacking,
	Dying
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
	public float MovementSpeed = 1f;
	const float MAX_HP = 10f;
	public float HP = 10f;

	public ECharacterCondition CurrentCondition = ECharacterCondition.Alive;
	public ECharacterState CurrentState = ECharacterState.Idle;
	public ECharacterState PreviousState = ECharacterState.Idle;
	public ECharacterDirection CurrentDirection = ECharacterDirection.Left;

	private Animator myAnimation;
	public RedBlink MyBlink;

	public Vector3 Destination;
	public EnemyTree TargetTree = null;
	public Bomb TargetSeed = null;

	public List<EnemyTree> CollidingTrees = new List<EnemyTree>();
	public List<Bomb> CollidingSeeds = new List<Bomb>();

	public List<GameObject> DamagePoints = new List<GameObject>();
	
	// Use this for initialization
	void Start () 
	{
		GameController.Instance.HealthBar.fillAmount = HP/MAX_HP;
		myAnimation = GetComponent<Animator>();
	}

	void Update()
	{
		if(CurrentState == ECharacterState.Dying)
			return;

		if(TargetTree != null)
		{
			if(CollidingTrees.Contains(TargetTree))
			{

				PreviousState = CurrentState;
				CurrentState = ECharacterState.Attacking;
				PlayAttackAnimation();
			}
			else
			{
				PreviousState = CurrentState;
			}
		}

		if(TargetSeed != null)
		{
			if(CollidingSeeds.Contains(TargetSeed))
			{		
				PreviousState = CurrentState;
				CurrentState = ECharacterState.Attacking;
				PlayAttackAnimation();
			}
			else
			{
				PreviousState = CurrentState;
			}
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
		//SoundsController.Instance.PlayChopSound();
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
		if(CurrentState == ECharacterState.Dying)
			return;

		Destination = target;

		PreviousState = CurrentState;
		CurrentState = ECharacterState.Moving;
	}

	public void Attack(EnemyTree target)
	{
		if(CurrentState == ECharacterState.Dying)
			return;

		TargetTree = target;
		Destination = target.transform.position;
		
		PreviousState = CurrentState;
		CurrentState = ECharacterState.Moving;
	}

	public void AttackSeed(Bomb target)
	{
		if(CurrentState == ECharacterState.Dying)
			return;
		
		TargetSeed = target;
		Destination = target.transform.position;
		
		PreviousState = CurrentState;
		CurrentState = ECharacterState.Moving;
	}

	public void HandleAttackFinished()
	{
		PreviousState = CurrentState;
		CurrentState = ECharacterState.Idle;
		PlayIdleAnimation();
		List<EnemyTree> trees = new List<EnemyTree>();
		trees.AddRange(CollidingTrees);
		foreach(var tree in trees)
		{
			tree.DecreaseHP();
		}
		List<Bomb> seeds = new List<Bomb>();
		seeds.AddRange(CollidingSeeds);
		foreach(var seed in seeds)
		{
			seed.DecreaseHP();
		}
	}

	public void DecreaseHP(float damage = 1f)
	{
		HP -= damage;
		GameController.Instance.HealthBar.fillAmount = HP/MAX_HP;
		MyBlink.Blink();
		GameController.Instance.HealthBar.GetComponent<RedBlink>().Blink();
		GameController.Instance.HealthBarBG.GetComponent<RedBlink>().Blink();
		if(HP <= 0f)
		{
			CurrentState = ECharacterState.Dying;
			myAnimation.Play("die");
		}
	}

	public void Die()
	{
		GameController.Instance.EndGame();
	}

	
	public void PlayChopSound()
	{
		SoundsController.Instance.PlayChopSound();
	}
}