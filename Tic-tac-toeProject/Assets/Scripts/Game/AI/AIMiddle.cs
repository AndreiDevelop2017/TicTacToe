using UnityEngine;

public class AIMiddle : IControllerAI 
{
	public void Move(CellsManager cellManager)
	{
		cellManager.BestOrRandomMove ();
	}
}
