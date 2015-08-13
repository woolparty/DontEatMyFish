using UnityEngine;
using System.Collections;

public class BlueFish : Fish {
    public override void Effect()
    {

        GameManager.GetInstance().AddScore(m_score);
    }

}
