using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class AIMoveCell
{
	public int index;
	public int score;

	public AIMoveCell(int bufIndex = 0, int bufScore = 0)
	{
		index = bufIndex;
		score = bufScore;
	}
}

public class CellsManager : MonoBehaviour 
{
	public const int CELLS_COUNT = 9;
	private const int CENTRALL_ELEMENT = 4;
	private const float DELAY_BEST_MOVE = 0.1f;

	[SerializeField]
	private GameObject _cellPrefab = null;

	private Transform _currentTransform;
	private Cell[] _cells;

	private int[] corners = new int[]{ 0, 2, 6, 8 };
	private int[,] _winCellCombinations = new int [, ] 
	{
		{ 0, 1, 2 },
		{ 3, 4, 5 },
		{ 6, 7, 8 },
		{ 0, 3, 6 },
		{ 1, 4, 7 },
		{ 2, 5, 8 },
		{ 0, 4, 8 },
		{ 2, 4, 6 }
	};

	void Awake () 
	{
		_currentTransform = GetComponent<Transform> ();

		_cells = new Cell[CELLS_COUNT];

		//заполняем поле ячейками
		for (int i = 0; i < CELLS_COUNT; i++) 
		{
			GameObject buf = Instantiate (_cellPrefab) as GameObject;

			buf.transform.SetParent (_currentTransform);
			_cells [i] = buf.GetComponent<Cell> ();
			_cells [i].num = i;
		}
	}
		
	//ищем выигрышную комбинацию
	public bool FindWinCellCombination(Cell []cells, CellState playerCellState)
	{
		bool rezult = false;
		for (int i = 0; i < _winCellCombinations.GetLength(0); i++) 
		{
			rezult = CheckCellsState (
				cells [_winCellCombinations [i, 0]].CurrentCellState,
				cells [_winCellCombinations [i, 1]].CurrentCellState,
				cells [_winCellCombinations [i, 2]].CurrentCellState,
				playerCellState);

			if (rezult) 
				break;
		}

		return rezult;
	}

	//выигрышная комбинацию ко всему полю
	public bool FindWinCellCombination(CellState playerCellState)
	{
		bool rezult = false;
		for (int i = 0; i < _winCellCombinations.GetLength(0); i++) 
		{
			rezult = CheckCellsState (
				_cells [_winCellCombinations [i, 0]].CurrentCellState,
				_cells [_winCellCombinations [i, 1]].CurrentCellState,
				_cells [_winCellCombinations [i, 2]].CurrentCellState,
				playerCellState);

			if (rezult) 
				break;
		}

		return rezult;
	}

	//проверяем состояния соседних ячеек 
	private bool CheckCellsState(CellState firstCell, CellState secondCell, CellState thirdCell, CellState playerCellState)
	{
		bool rezult = (firstCell == playerCellState && secondCell == playerCellState  && thirdCell == playerCellState) ? true : false;
		return rezult;
	}

	//возвращаем свободные ячейки
	public Cell[] ReturnFreeCells(Cell []cells)
	{
		return cells.Where (c => c.CurrentCellState == CellState.Free).ToArray ();
	}

	public void RandomMove()
	{
		var freeCells = ReturnFreeCells (_cells);

		if (freeCells.Length > 0) 
		{
			int i = Random.Range (0, freeCells.Length);

			freeCells [i].SetCurrentCellState ();
		}
	}
		
	public void BestMove()
	{
		StartCoroutine (BestMoveWithDelay ());
	}

	public void BestOrRandomMove()
	{
		if(Random.Range (0, 10) < 7)
			BestMove ();
		else
			RandomMove ();
	}

	IEnumerator BestMoveWithDelay()
	{
		yield return new WaitForSeconds(DELAY_BEST_MOVE);

		Cell[] freeCells = ReturnFreeCells (_cells);
		int index;

		//если первым ходит ИИ, ход в угол
		if (freeCells.Length == CELLS_COUNT)
			index = RandomCorner ();
		//Ии ходит вторым
		else if (freeCells.Length == CELLS_COUNT - 1) 
		{
			if (freeCells.Contains (_cells [CENTRALL_ELEMENT]))
				index = CENTRALL_ELEMENT;
			else
				index = RandomCorner ();
		}
		else 
			index = MiniMax (_cells, CellState.Zero).index;
		
		_cells [index].SetCurrentCellState ();
	}

	#region MinMax
	private AIMoveCell MiniMax(Cell[] cells, CellState cellType)
	{
		int prevScore = 1;

		Cell[] freeCells = ReturnFreeCells (cells);

		if (FindWinCellCombination (CellState.Cross))
			return new AIMoveCell(0, -10);
		else if (FindWinCellCombination (CellState.Zero))
			return new AIMoveCell(0, 10);
		else if (freeCells.Length == 0)
			return new AIMoveCell(0, 0);

		List<AIMoveCell> AImoves = new List<AIMoveCell>();

		// цикл по доступным клеткам
		for (int i = 0; i < freeCells.Length; i++)
		{
			AIMoveCell move = new AIMoveCell();

			move.index = freeCells [i].num;
			CellState bufState = cells.Where (c => c.num == move.index).Single ().CurrentCellState;

			// совершить ход за текущего игрока
			cells.Where(c => c.num == move.index).Single().SetCurrentCellState(cellType);

			//получить очки, заработанные после вызова минимакса от противника текущего игрока
			if (cellType == CellState.Zero)
				prevScore = MiniMax (cells, CellState.Cross).score;
			else if (cellType == CellState.Cross)
				prevScore = MiniMax (cells, CellState.Zero).score;
			
			move.score = prevScore;

			//вернуть клетку в предыдущее состояние
			cells.Where(c => c.num == move.index).Single().SetCurrentCellState(bufState);

			// положить объект в массив
			AImoves.Add(move);
		}

		// если это ход ИИ, пройти циклом по ходам и выбрать ход с наибольшим количеством очков
		int bestMove = 0;

		if(cellType == CellState.Zero)
		{
			int bestScore = -10000;
			for(var i = 0; i < AImoves.Count; i++)
			{
				if(AImoves[i].score > bestScore)
				{
					bestScore = AImoves[i].score;
					bestMove = i;
				}
			}
		}
		else
		{
			// иначе пройти циклом по ходам и выбрать ход с наименьшим количеством очков
			int bestScore = 10000;
			for(var i = 0; i < AImoves.Count; i++)
			{
				if(AImoves[i].score < bestScore)
				{
					bestScore = AImoves[i].score;
					bestMove = i;
				}
			}
		}

		// вернуть выбранный ход (объект) из массива ходов
		return AImoves.ElementAt(bestMove);
	}
	#endregion

	private int RandomCorner()
	{
		return corners[Random.Range(0, corners.Length)];
	}
}
