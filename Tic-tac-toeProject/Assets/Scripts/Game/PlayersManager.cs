using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour 
{
	private const string AI_PATH = "AI";

	public delegate void PlayerSetHandler();
	public static event PlayerSetHandler OnPlayersSet;

	public Player FirstPlayer{ get; private set;}
	public Player SecondPlayer{ get; private set;}

	public ScoreText oponentScore;

	private GameObject _aiPrefab;
	// Use this for initialization
	void Start () 
	{
		_aiPrefab = Resources.Load (AI_PATH) as GameObject;
			
		FirstPlayer = new Player ();

		if (GameObject.FindObjectOfType<LevelChoise> ()) 
		{
			SecondPlayer = LevelChoise.Instance.OponentPlayer;

			if (SecondPlayer.PlayerType == PlayerType.AI)
				Instantiate (_aiPrefab);
		}
		else
			SecondPlayer = new Player (PlayerType.SecondPlayerHuman, Difficult.None, CellState.Zero);

		oponentScore.playerTypeScore = SecondPlayer.PlayerType;

		if (OnPlayersSet != null)
			OnPlayersSet ();
	}
}
