using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIImposible : IControllerAI 
{
	public void Move(CellsManager cellManager)
	{
		cellManager.BestMove ();
	}
}
