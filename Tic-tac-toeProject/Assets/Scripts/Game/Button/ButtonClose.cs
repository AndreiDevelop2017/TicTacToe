using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClose : Button, IPointerDownHandler, IPointerUpHandler 
{
	public delegate void ButtonCloseHandler();
	public static event ButtonCloseHandler OnButtonClosePress;

	public void OnPointerDown (PointerEventData eventData)
	{
		OnButtonDownColorChange ();
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		OnButtonUpColorChange ();

		if (OnButtonClosePress != null)
			OnButtonClosePress ();
	}
}
