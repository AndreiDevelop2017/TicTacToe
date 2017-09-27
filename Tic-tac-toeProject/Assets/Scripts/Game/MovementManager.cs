using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour 
{
	private const float DELAY_CHANGE_PLAYER = 0.1f;

	public delegate void PlayerWinHandler(Player player);
	public static event PlayerWinHandler OnPlayerWin;

	public delegate void PlayerChangeHandler(Player player);
	public static event PlayerChangeHandler OnChangedPlayer;

	public delegate void MovementCountHandler();
	public static event MovementCountHandler OnMovementCountEqualZero;

	private Player _firstPlayer;
	private Player _secondPlayer;

	//текущий игрок
	private Player _currentPlayer;

	private int _currentMovementCount;
	private CellsManager _currentCellsManager;
	private bool _playerWin;

	private PlayersManager _playersManager;

	void OnEnable()
	{
		Cell.OnCellStateChanged += GoToNextMovement;
		PlayersManager.OnPlayersSet += SetCurrentPlayer;

		ButtonClose.OnButtonClosePress += ResetCountMovement;
	}

	void OnDisable()
	{
		Cell.OnCellStateChanged -= GoToNextMovement;
		PlayersManager.OnPlayersSet -= SetCurrentPlayer;

		ButtonClose.OnButtonClosePress -= ResetCountMovement;
	}

	void Start()
	{
		_playersManager = GameObject.FindObjectOfType<PlayersManager> ();

		_currentMovementCount = CellsManager.CELLS_COUNT;
		_currentCellsManager = GetComponent<CellsManager> ();
	}

	private void SetCurrentPlayer()
	{
		_firstPlayer = _playersManager.FirstPlayer;
		_secondPlayer = _playersManager.SecondPlayer;

		_currentPlayer = _firstPlayer;
	}

	//проверка победы игрока
	private void CheckWinPlayer()
	{
		if (!_playerWin) 
		{
			_playerWin = _currentCellsManager.FindWinCellCombination (_currentPlayer.PlayerCellType);

			if (_playerWin) 
				if (OnPlayerWin != null)
					OnPlayerWin (_currentPlayer);
		}
	}

	//смена игрока
	private void ChangePlayer(bool Change = true)
	{
		if(Change)
			_currentPlayer = (_currentPlayer == _firstPlayer) ? _secondPlayer : _firstPlayer;

		//после смены игрока
		if (OnChangedPlayer != null)
			OnChangedPlayer (_currentPlayer);
	}

	//уменьшение оставшихся ходов
	private void DecreaseCurrentMovement()
	{
		_currentMovementCount--;
		if (_currentMovementCount == 0) 
		{
			//ничья
			if (OnPlayerWin != null)
				OnPlayerWin (new Player(PlayerType.None));

			if (OnMovementCountEqualZero != null)
				OnMovementCountEqualZero ();
		}
	}

	private void GoToNextMovement()
	{
		CheckWinPlayer ();

		if (!_playerWin) 
		{
			DecreaseCurrentMovement ();

			if (_currentMovementCount > 0)
				ChangePlayer ();
		}
	}

	private void ResetCountMovement()
	{
		StartCoroutine (DelayChangePlayer (!_playerWin));
		
		_currentMovementCount = CellsManager.CELLS_COUNT;
		_playerWin = false;
	}

	IEnumerator DelayChangePlayer(bool Change)
	{
		yield return new WaitForSeconds (DELAY_CHANGE_PLAYER);
		ChangePlayer (Change);
	}
}
