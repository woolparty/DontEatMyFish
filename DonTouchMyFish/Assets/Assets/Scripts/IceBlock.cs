using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {
	
	public FishType foodType;

    [HideInInspector]
	public Material[] blockMaterials;

    [HideInInspector]
    public bool m_isBottom = false;

    private MeshRenderer renderer;

	// Use this for initialization
	void Awake () {
		foodType = FishType.RedFish;
		renderer = GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void CheckForDestroy()
    {
        Vector3 pos = transform.position;
        if (pos.x <= -7f || pos.x > 5f)
        {
            GameManager.GetInstance().m_IceBlockManager.DeleteBlock(this);
            GameManager.GetInstance().m_IceBlockManager.CheckForMatch();
        }
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
