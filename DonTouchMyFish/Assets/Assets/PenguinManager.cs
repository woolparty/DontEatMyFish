using UnityEngine;
using System.Collections;

public class PenguinManager : MonoBehaviour {

	public Transform m_TransLeft;
	public Transform m_TransRight;

	public PenguinController m_leftPanguin;
	public PenguinController m_RightPanguin;

	public void FeedPanguin(bool i_isLeft, FishType i_type)
	{
		if(i_isLeft)
			m_leftPanguin.Eat(i_type);
		else
			m_RightPanguin.Eat(i_type);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
