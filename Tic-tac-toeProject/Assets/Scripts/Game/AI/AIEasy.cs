using UnityEngine;

public class AIEasy : IControllerAI 
{
	public void Move(CellsManager cellManager)
	{
		cellManager.RandomMove ();
	}
}
