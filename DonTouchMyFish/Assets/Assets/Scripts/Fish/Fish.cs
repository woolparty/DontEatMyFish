using UnityEngine;
using System.Collections;

public class Fish
{
    public FishType m_foodType;
    public int      m_score = 10;

    public virtual void Init(FishType i_type)
    {
        m_foodType = i_type;

    }


    public virtual void Effect()
    {
        //FishEffect!!!
        //GameManager.GetInstance().AddScore(m_score);
    }
}
