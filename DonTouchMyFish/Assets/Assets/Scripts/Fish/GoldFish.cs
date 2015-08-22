using UnityEngine;
using System.Collections;

public class GoldFish : Fish
{
	public override void Effect()
	{
		
		GameManager.GetInstance().AddScore(m_score);
	}
	
	
	
	
}
