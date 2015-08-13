using UnityEngine;
using System.Collections;

public class RedFish : Fish
{
    public override void Effect()
    {

        GameManager.GetInstance().AddScore(m_score);
    }




}
