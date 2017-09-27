using UnityEngine;

public class LevelChoise : Singleton<LevelChoise>
{
	public delegate void LevelChoiseHandler();
	public static LevelChoiseHandler OnLevelChoised;

	public Player OponentPlayer{ get; private set;}
	private Difficult _currentDifficult;
	private PlayerType _currentOponentPlayer;

	void OnEnable()
	{
		ButtonOponentChoise.OnButtonOponentPress += SetOponent;
		ButtonDifficultChoise.OnButtonDifficultPress += SetDifficult;
	}

	void OnDisable()
	{
		ButtonOponentChoise.OnButtonOponentPress -= SetOponent;
		ButtonDifficultChoise.OnButtonDifficultPress -= SetDifficult;
	}

	public void SetDifficult(Difficult currentDifficult)
	{
		_currentDifficult = currentDifficult;

		OponentPlayer = new Player (_currentOponentPlayer, _currentDifficult, CellState.Zero);

		if (OnLevelChoised != null)
			OnLevelChoised ();
	}

	public void SetOponent(PlayerType currentOponentPlayer)
	{
		_currentOponentPlayer = currentOponentPlayer;

		if (currentOponentPlayer == PlayerType.SecondPlayerHuman) 
			SetDifficult (Difficult.None);
	}
}
