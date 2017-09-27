using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour 
{
	private const float DELAY_MOVE_AFTER_RESTART = 0.1f;
	private const string AI_THINK = "AIThink";
	private const float AI_THINK_INVISIBLE_Z = 10000f;

	public delegate void AIMoveHandler();
	public static event AIMoveHandler OnAIMove;

	private IControllerAI _aiBehavior;
	private PlayersManager _playersManager;
	private CellsManager _cellManager;
	private Player _playerWin; 

	void Awake()
	{
		_playersManager = GameObject.FindObjectOfType<PlayersManager> ();
		_cellManager = GameObject.FindObjectOfType<CellsManager> ();

		switch (_playersManager.SecondPlayer.PlayerDifficult) 
		{
			case Difficult.Easy:
				_aiBehavior = new AIEasy ();
				break;
			case Difficult.Middle:
				_aiBehavior = new AIMiddle ();
				break;
			case Difficult.Imposible:
				_aiBehavior = new AIImposible ();
				break;
		}
	}

	void OnEnable()
	{
		MovementManager.OnChangedPlayer += Move;
	}

	void OnDisable()
	{
		MovementManager.OnChangedPlayer -= Move;
	}

	private void Move(Player player)
	{
		if (player.PlayerType == PlayerType.AI) 
		{
			_aiBehavior.Move (_cellManager);

			if (OnAIMove != null)
				OnAIMove ();
		}
	}
}
