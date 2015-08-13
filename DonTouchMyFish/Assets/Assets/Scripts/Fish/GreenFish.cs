using UnityEngine;
using System.Collections;

public class GreenFish : Fish {

    public override void Effect()
    {

        GameManager.GetInstance().AddScore(m_score);
    }


}
