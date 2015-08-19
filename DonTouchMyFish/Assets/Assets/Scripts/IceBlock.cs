﻿using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour
{

    public Fish m_fish;
    public bool m_isMatched = false;

    [HideInInspector]
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
        if (!m_isMatched&&(pos.x <= -5f || pos.x > 5f))
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
