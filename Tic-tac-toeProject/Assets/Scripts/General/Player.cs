public enum Difficult
{
	Easy,
	Middle,
	Imposible,
	None
}

public enum PlayerType
{
	FirstPlayerHuman,
	SecondPlayerHuman,
	AI,
	None
}

public class Player
{
	//тип игрока
	public PlayerType PlayerType;

	//сложность игрока
	public Difficult PlayerDifficult;

	//тип ячеек игрока
	public CellState PlayerCellType;

	public Player(
		PlayerType playerType = PlayerType.FirstPlayerHuman, 
		Difficult playerDifficult = Difficult.None,
		CellState playerCellState = CellState.Cross)
	{
		PlayerType = playerType;
		PlayerCellType = playerCellState;
		PlayerDifficult = playerDifficult;
	}
}
