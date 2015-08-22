using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour
{

    public Fish m_fish;
    public bool m_isMatched = false;

    //[HideInInspector]
	public Material[] blockMaterials;

    [HideInInspector]
    public bool m_isBottom = false;

	public GameObject m_blockObject;

    private MeshRenderer renderer;

	// Use this for initialization
	void Awake () {

		renderer = GetComponentInChildren<MeshRenderer>();
	}

    public FishType GetFishType()
    {
        return m_fish.m_foodType;
    }

	void Update()
	{

	}

    public void CheckForDestroy()
    {
        Vector3 pos = transform.position;
		if( !m_isMatched && pos.x <= -5f )
			GetEaten(true);
		if( !m_isMatched && pos.x >=  5f )
			GetEaten(false);

    }

    public void GetEaten(bool i_isLeft)
    {
        m_fish.Effect();
		GameManager gm =  GameManager.GetInstance();
		gm.m_IceBlockManager.DeleteBlock(this);
		gm.m_PenguinManager.FeedPanguin(i_isLeft,m_fish.m_foodType);
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
			case FishType.CopperFish:
				GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[3];
				m_fish = new CopperFish();
				m_fish.Init(FishType.CopperFish);
				break;
			case FishType.SilverFish:
				GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[4];
				m_fish = new SilverFish();
				m_fish.Init(FishType.SilverFish);
				break;
			case FishType.GoldFish:
				GetComponentInChildren<MeshRenderer>().sharedMaterial = blockMaterials[5];
				m_fish = new GoldFish();
				m_fish.Init(FishType.GoldFish);
				break;
		}
		
	}


    public void SetRandomType()
	{
		int typeInt = Random.Range(0, 3);

        SetType((FishType)typeInt);

	}
	bool isFlashing = false;
	public void Flash()
	{
		if(!isFlashing)
			InvokeRepeating("FlashHelper",0,0.05f);
		isFlashing = true;
	}

	private void FlashHelper()
	{
		m_blockObject.SetActive(!m_blockObject.activeSelf);
	}
}
