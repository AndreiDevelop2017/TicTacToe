using UnityEngine;

public class UIMenu : MonoBehaviour 
{
	private Animator _currentAnim;

	void Awake()
	{
		_currentAnim = GetComponent<Animator> ();
	}

	void OnEnable()
	{
		ButtonOponentChoise.OnButtonOponentPress += ShowDifficultChoise;
	}

	void OnDisable()
	{
		ButtonOponentChoise.OnButtonOponentPress -= ShowDifficultChoise;
	}

	private void ShowDifficultChoise(PlayerType playerType)
	{
		if (playerType == PlayerType.AI) 
			_currentAnim.SetTrigger (AnimationConstant.DIFFICULT_SHOW);
	}
}
