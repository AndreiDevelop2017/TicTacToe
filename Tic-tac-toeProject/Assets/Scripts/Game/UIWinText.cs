using UnityEngine;
using TMPro;

public class UIWinText : MonoBehaviour 
{
	private const string FIRST_PLAYER_WIN = "First player win";
	private const string SECOND_PLAYER_WIN = "Second player win";
	private const string AI_WIN = "AI win";
	private const string DRAW = "Draw!";

	private TextMeshProUGUI _wintext;

	void Awake()
	{
		_wintext = GetComponent<TextMeshProUGUI> ();
	}

	void OnEnable()
	{
		MovementManager.OnPlayerWin += TextLabelChose;
	}

	void OnDisable()
	{
		MovementManager.OnPlayerWin -= TextLabelChose;
	}

	private void TextLabelChose(Player player)
	{
		switch (player.PlayerType) 
		{
			case PlayerType.FirstPlayerHuman:
				_wintext.text = FIRST_PLAYER_WIN;
				break;
			case PlayerType.SecondPlayerHuman:
				_wintext.text = SECOND_PLAYER_WIN;
				break;
			case PlayerType.AI:
				_wintext.text = AI_WIN;
				break;
			case PlayerType.None:
				_wintext.text = DRAW;
				break;
		}
	}
}
