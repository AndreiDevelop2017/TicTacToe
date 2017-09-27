using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBack : Button, IPointerDownHandler, IPointerUpHandler 
{
	public delegate void ButtonBackHandler();
	public static event ButtonBackHandler OnButtonBackPress;

	public void OnPointerDown (PointerEventData eventData)
	{
		OnButtonDownColorChange ();
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		OnButtonUpColorChange ();

		if (OnButtonBackPress != null)
			OnButtonBackPress ();
	}
}
