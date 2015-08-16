using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        IceBlock iceBlockScript = collision.gameObject.GetComponent<IceBlock>();

        if (!iceBlockScript.m_isBottom)
        {

			Destroy(iceBlockScript.gameObject);

			foreach (ContactPoint contact in collision.contacts)
			{
				GameManager.GetInstance().m_specialEffectManager.PlayEffectAt(SpecialEffect.CrashedEffect,contact.point);
			}

			GameManager.GetInstance().m_IceBlockManager.DestroyLastFloor();
            GameManager.GetInstance().GameOver();

        }

    }
}
