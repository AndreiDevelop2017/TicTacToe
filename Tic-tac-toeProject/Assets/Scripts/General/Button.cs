using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour 
{
	private Color _newColor = new Color (50f, 50f, 50f, 0.7f);
	private Color _oldColor;
	private Image _currentImage;

	void Awake()
	{
		_currentImage = GetComponent<Image> ();
		_oldColor = _currentImage.color;
	}

	protected void OnButtonUpColorChange()
	{
		_currentImage.color = _oldColor;
	}

	protected void OnButtonDownColorChange()
	{
		_currentImage.color = _newColor;
	}

}
