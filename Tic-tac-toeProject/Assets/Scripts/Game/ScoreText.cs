using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour 
{
	public PlayerType playerTypeScore;

	private int _score;
	private TextMeshProUGUI _textMPro;

	void Awake()
	{
		_score = 0;
		_textMPro = GetComponent<TextMeshProUGUI> ();
	}

	void OnEnable()
	{
		MovementManager.OnPlayerWin += ChangeScore;
	}

	void OnDisable()
	{
		MovementManager.OnPlayerWin -= ChangeScore;
	}

	private void ChangeScore(Player player)
	{
		if (player.PlayerType == playerTypeScore) 
		{
			_score++;
			_textMPro.text = _score.ToString ();
		}
	}
}
