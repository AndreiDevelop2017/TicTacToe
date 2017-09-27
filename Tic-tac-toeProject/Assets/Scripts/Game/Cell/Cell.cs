using System.Collections;
using UnityEngine;

public enum CellState
{
	Free,
	Cross,
	Zero
}

public class Cell : MonoBehaviour
{
	public delegate void CellStateChangeHandler();
	public static event CellStateChangeHandler OnCellStateChanged;

	//текущее состояние ячейки
	public CellState CurrentCellState { get ; private set ;}

	public int num;					

	private GameObject _cross;
	private GameObject _zero;
	private Transform _currentTransform;

	//потенциальное состояние ячейки
	private CellState _nextCellState;		

	void Awake()
	{
		gameObject.AddComponent<CellButton> ();

		_currentTransform = GetComponent<Transform> ();

		_cross = _currentTransform.GetChild (0).gameObject;
		_zero = _currentTransform.GetChild (1).gameObject;
		_nextCellState = CellState.Cross;
		CurrentCellState = CellState.Free;
	}

	void OnEnable()
	{
		MovementManager.OnChangedPlayer += SetNextCellState;
		ButtonClose.OnButtonClosePress += ResetToFreeCurrentCellState;
	}

	void OnDisable()
	{
		MovementManager.OnChangedPlayer -= SetNextCellState;
		ButtonClose.OnButtonClosePress -= ResetToFreeCurrentCellState;
	}

	public void SetCurrentCellState()
	{
		if (CurrentCellState == CellState.Free) 
		{
			CurrentCellState = _nextCellState;

			SetCellImage ();

			if (OnCellStateChanged != null)
				OnCellStateChanged ();
		}
	}

	private void SetCellImage()
	{
		if (CurrentCellState == CellState.Cross)
			_cross.SetActive (true);
		else if(CurrentCellState == CellState.Zero)
			_zero.SetActive (true);
	}

	public void SetCurrentCellState(CellState bufState)
	{
		CurrentCellState = bufState;
	}

	private void SetNextCellState(Player player)
	{
		_nextCellState = player.PlayerCellType;
	}

	private void ResetToFreeCurrentCellState()
	{
		_cross.SetActive (false);
		_zero.SetActive (false);
		CurrentCellState = CellState.Free;
	}
}
