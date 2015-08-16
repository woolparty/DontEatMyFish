using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour
{

    public Fish m_fish;
    public bool m_isMatched = false;

    [HideInInspector]
	public Material[] blockMaterials;

    [HideInInspector]
    public bool m_isBottom = false;

    private MeshRenderer renderer;

	// Use this for initialization
	void Awake () {

		renderer = GetComponentInChildren<MeshRenderer>();
	}

    public FishType GetFishType()
    {
        return m_fish.m_foodType;
    }

    public void CheckForDestroy()
    {
        Vector3 pos = transform.position;
        if (pos.x <= -7f || pos.x > 5f)
        {
            GetEaten();
        }
    }

    public void GetEaten()
    {
        m_fish.Effect();
        GameManager.GetInstance().m_IceBlockManager.DeleteBlock(this);
        //GameManager.GetInstance().m_IceBlockManager.CheckForMatch();
    }

    public void SetType(FishType i_type)
    {
        switch (i_type)
        {
            case FishType.RedFish:
                GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[0];
                m_fish = new RedFish();
                m_fish.Init(FishType.RedFish);
                break;
            case FishType.GreenFish:
                GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[1];
                m_fish = new GreenFish();
                m_fish.Init(FishType.GreenFish);
                break;
            case FishType.BlueFish:
                GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[2];
                m_fish = new BlueFish();
                m_fish.Init(FishType.BlueFish);
                break;

        }

    }

	public int counter = 0;
	public void OnCollisionExit(Collision collision)
	{
		counter--;

	}

	public void OnCollisionEnter(Collision collision)
	{
		counter++;
		if(counter >= 2)
			GameManager.GetInstance().m_IceBlockManager.CheckForMatch();
	}

    public void SetRandomType()
	{
		int typeInt = Random.Range(0, 3);

        SetType((FishType)typeInt);

	}
}
