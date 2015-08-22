using UnityEngine;
using System.Collections;


public class SilverFish : Fish
{
	public override void Effect()
	{
		
		GameManager.GetInstance().AddScore(m_score);
	}
	
	
	
	
}
