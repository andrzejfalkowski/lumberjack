using UnityEngine;
using System.Collections;

public class BG : MonoBehaviour 
{
	[SerializeField]
	private Texture2D moveCursor;
	[SerializeField]
	private Texture2D blockedCursor;

	void OnMouseEnter()
	{
		if(GameController.Instance.MainCharacter.CurrentCondition != ECharacterCondition.Dead &&
		   GameController.Instance.MainCharacter.CurrentState != ECharacterState.Idle)
		{
			Cursor.SetCursor(moveCursor, Vector2.zero, CursorMode.Auto);
		}
	}

	void OnMouseExit()
	{
		if(GameController.Instance.MainCharacter.CurrentCondition != ECharacterCondition.Dead &&
		   GameController.Instance.MainCharacter.CurrentState != ECharacterState.Idle)
		{
			Cursor.SetCursor(blockedCursor, Vector2.zero, CursorMode.Auto);
		}
	}

	void OnMouseDown()
	{
		//Debug.Log ("Clicked on " + this.gameObject.name);
		float mouseX = (Input.mousePosition.x);
		float mouseY = (Input.mousePosition.y);
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0));
		GameController.Instance.MainCharacter.GoToSpot(mousePosition);
	}
}
