using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOponentChoise : Button, IPointerDownHandler, IPointerUpHandler
{
	public delegate void ButtonOponentHandler(PlayerType playerType);
	public static event ButtonOponentHandler OnButtonOponentPress;

	[SerializeField]
	private PlayerType _currentOponentPlayer = PlayerType.None;

	public void OnPointerDown (PointerEventData eventData)
	{
		OnButtonDownColorChange ();
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		OnButtonUpColorChange ();

		if (OnButtonOponentPress != null)
			OnButtonOponentPress (_currentOponentPlayer);
	}
}
