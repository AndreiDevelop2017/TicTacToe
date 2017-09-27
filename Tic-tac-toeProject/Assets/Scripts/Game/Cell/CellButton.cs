using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private Image _currentImage;
	private bool _canEnableCurrentCell;
	private Cell _currentCell;

	void Start()
	{
		_currentImage = GetComponent<Image> ();
		_currentCell = GetComponent<Cell> ();
		_canEnableCurrentCell = true;
	}

	void OnEnable()
	{
		MovementManager.OnChangedPlayer += SwitchAllCellsRaycastTarger;
		MovementManager.OnMovementCountEqualZero += DisableAllCellsRaycastTarget;

		ButtonClose.OnButtonClosePress += EnableAllCellsRaycastTarget;
	}

	void OnDisable()
	{
		MovementManager.OnChangedPlayer -= SwitchAllCellsRaycastTarger;
		MovementManager.OnMovementCountEqualZero -= DisableAllCellsRaycastTarget;

		ButtonClose.OnButtonClosePress -= EnableAllCellsRaycastTarget;
	}

	public void OnPointerDown (PointerEventData eventData){}

	public void OnPointerUp (PointerEventData eventData)
	{
		_canEnableCurrentCell = false;
		_currentCell.SetCurrentCellState ();
		_currentImage.raycastTarget = false;
	}

	private void SwitchAllCellsRaycastTarger(Player player)
	{
		if (_canEnableCurrentCell) 
		{
			 if(player.PlayerType == PlayerType.AI)
				_currentImage.raycastTarget = false;
			else
				_currentImage.raycastTarget = true;
		}
	}

	private void EnableAllCellsRaycastTarget()
	{
		_currentImage.raycastTarget = true;
		_canEnableCurrentCell = true;
	}

	private void DisableAllCellsRaycastTarget()
	{
		_currentImage.raycastTarget = false;
		_canEnableCurrentCell = false;
	}
}
