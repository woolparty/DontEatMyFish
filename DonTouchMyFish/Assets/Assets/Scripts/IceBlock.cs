using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {
	//[HideInInspector]
	public FishType foodType;
	MeshRenderer renderer;
	public Material[] blockMaterials;
	// Use this for initialization
	void Awake () {
		foodType = FishType.RedFish;
		renderer = GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if( pos.x <= -7f || pos.x > 5f )
			GameManager.GetInstance().m_IceBlockManager.DeleteBlock(this);
	}

	public void SetRandomType()
	{
		int type = Random.Range(1, 4);
//		Debug.Log("Type: "+ type);
		switch(type)
		{
			case 1:
				GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[0];
				foodType = FishType.RedFish;
				break;
			case 2:
				GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[1];
				foodType = FishType.GreenFish;
				break;
			case 3:
				GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[2];
				foodType = FishType.BlueFish;
				break;
		}
	}
}
