using UnityEngine;

public class UIGame : MonoBehaviour 
{
	private Animator _currentAnim;
	private bool _showWin;

	void Awake()
	{
		_currentAnim = GetComponent<Animator> ();
	}

	void OnEnable()
	{
		MovementManager.OnPlayerWin += ShowWin;
		ButtonClose.OnButtonClosePress += HideWin;
	}

	void OnDisable()
	{
		MovementManager.OnPlayerWin -= ShowWin;
		ButtonClose.OnButtonClosePress -= HideWin;
	}

	private void ShowWin(Player player)
	{
		if (!_showWin) 
		{
			_currentAnim.SetTrigger (AnimationConstant.WIN_SHOW);
			_showWin = true;
		}
	}

	private void HideWin()
	{
		_currentAnim.SetTrigger (AnimationConstant.WIN_HIDE);
		_showWin = false;
	}
}
