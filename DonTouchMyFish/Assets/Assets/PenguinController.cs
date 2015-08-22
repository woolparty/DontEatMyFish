using UnityEngine;
using System.Collections;

public class PenguinController : MonoBehaviour {

	public int m_satisfaction = 0;
	public FishType m_preferredFish;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Eat(FishType i_fish)
	{return;
		if( i_fish == m_preferredFish )
			m_satisfaction += 10;
		else
			m_satisfaction -= 10;

		m_satisfaction = Mathf.Max(m_satisfaction,0);

		if(m_satisfaction >= 50)
			Leave();

	}

	public void Leave()
	{
		Debug.Log("leave!!!");
		GameManager.GetInstance().m_IceBlockManager.DestroyType(m_preferredFish);
	}
}
