using UnityEngine;
using System.Collections;




public class SpecialEffectManager : MonoBehaviour
{
    public GameObject MatchEffect;
	public GameObject CrashedEffect;

    public Transform m_Transform;

    public void PlayEffectAt(SpecialEffect i_effectName, Vector3 i_position)
    {
        GameObject effect = null;
        
        switch (i_effectName)
        {
            case SpecialEffect.MatchedEffect:
                effect = Instantiate(MatchEffect) as GameObject;
                break;


            case SpecialEffect.CrashedEffect:
				effect = Instantiate(CrashedEffect) as GameObject;
                break;
        }
        effect.transform.parent = m_Transform;
        effect.transform.position = i_position;


        Destroy(effect,3);
    }

}
