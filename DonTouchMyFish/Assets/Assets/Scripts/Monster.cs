using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public FishType foodType;

	public virtual void Eat(IceBlock iceBlock)
	{
		if(iceBlock.GetFishType() == foodType)
		{ 
			Debug.Log("Fucking good."); 
			GameManager.GetInstance().AddScore(10);
		}
		else{
			Debug.Log("Wrong bitch.");
			GameManager.GetInstance().AddScore(-10);

		}
		Destroy(iceBlock.gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.GetComponent<IceBlock>() != null)
		{
			Eat(coll.gameObject.GetComponent<IceBlock>());
		}
	}
}
