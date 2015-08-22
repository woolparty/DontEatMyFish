using UnityEngine;
using System.Collections;

public class CopperFish : Fish {
	
	public override void Effect()
	{
		
		GameManager.GetInstance().AddScore(m_score);
	}
	
	
}
