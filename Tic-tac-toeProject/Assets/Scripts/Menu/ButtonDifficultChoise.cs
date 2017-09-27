using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDifficultChoise : Button, IPointerDownHandler, IPointerUpHandler
{
	public delegate void ButtonDiffucultHandler(Difficult dif);
	public static event ButtonDiffucultHandler OnButtonDifficultPress;

	[SerializeField]
	private Difficult _currentDifficult = Difficult.Easy;

	public void OnPointerDown (PointerEventData eventData)
	{
		OnButtonDownColorChange ();
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		OnButtonUpColorChange ();

		if (OnButtonDifficultPress != null)
			OnButtonDifficultPress (_currentDifficult);
	}
}
