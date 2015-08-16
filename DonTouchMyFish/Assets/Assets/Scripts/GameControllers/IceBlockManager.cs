using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IceBlockManager : MonoBehaviour
{

    private List<IceBlock> m_iceBlocks;
    private List<IceBlock> m_matchedBlocks;

    
    public  GameObject     m_blockPrefab;

	public Transform blockDropPos;
	List<FishType> InitFishList = new List<FishType>();
	FishType lastBlockType;
	FishType lastlastBlockType;


	// Use this for initialization
	void Awake ()
	{


	    m_iceBlocks = new List<IceBlock>();
        m_matchedBlocks = new List<IceBlock>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.U))
	    {
	        CheckForMatch();
	    }

		if (m_iceBlocks.Count >= 1)
			m_iceBlocks[0].m_isBottom = true;
	}

	public int GetBlockCount()
	{
		return m_iceBlocks.Count;
	}

    public void AddBlock(IceBlock i_iceblock)
    {
        m_iceBlocks.Add(i_iceblock);
        i_iceblock.transform.parent = transform;
    }

	public void DeleteBlock(IceBlock i_iceblock)
	{
		m_iceBlocks.Remove(i_iceblock);
		Destroy(i_iceblock.gameObject);

        if (m_iceBlocks.Count >= 1)
            m_iceBlocks[0].m_isBottom = true;
	}
	
	public void Clear()
	{
		m_iceBlocks.Clear();
		m_matchedBlocks.Clear();
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	public void InitBlocks(int count, FishType type1, FishType type2)
	{


		//Init first two blocks
		float random = Random.Range(0, 0.999f);
		if (random > 0.5f)
		{
			//DropBlock(type1);
			InitFishList.Add(type1);
			lastlastBlockType = type1;
			Debug.Log("Drop: " + type1);
		}
		else
		{
			//DropBlock(type2);
			InitFishList.Add(type2);
			lastlastBlockType = type2;
			Debug.Log("Drop: " + type2);
		}

		random = Random.Range(0, 0.999f);
		if (random > 0.5f)
		{
			//DropBlock(type1);
			InitFishList.Add(type1);
			lastBlockType = type1;
			Debug.Log("Drop: " + type1);
		}
		else
		{
			//DropBlock(type2);
			InitFishList.Add(type2);
			lastBlockType = type2;
			Debug.Log("Drop: " + type2);
		}

		//Init remain blocks
		for(int i = 2; i< count; i++)
		{
			if(lastlastBlockType == lastBlockType)
			{
				if(lastlastBlockType == type1)
				{
					//DropBlock(type2);
					InitFishList.Add(type2);
					lastBlockType = type2;
					Debug.Log("Drop: " + type2);
				}
				else
				{
					//DropBlock(type1);
					InitFishList.Add(type1);
					lastBlockType = type1;
					Debug.Log("Drop: " + type1);
				}
			}
			else
			{
				random = Random.Range(0, 0.999f);
				if (random > 0.5f)
				{
					//DropBlock(type1);
					InitFishList.Add(type1);
					lastlastBlockType = lastBlockType;
					lastBlockType = type1;
					Debug.Log("Drop: " + type1);
				}
				else
				{
					//DropBlock(type2);
					InitFishList.Add(type2);
					lastlastBlockType = lastBlockType;
					lastBlockType = type2;
					Debug.Log("Drop: " + type2);
				}
			}
		}
	}

	public static FishType GetRandomFishType()
	{
		int type = Random.Range(1, 4);
		//		Debug.Log("Type: "+ type);
		switch (type)
		{
			case 1:
				return FishType.RedFish;
			case 2:
				return FishType.GreenFish;
			case 3:
				return FishType.BlueFish;
		}
		return 0;
	}

	public static FishType GetOneOfTwoFishType(FishType type1, FishType type2)
	{
		int type = Random.Range(1, 3);
		//		Debug.Log("Type: "+ type);
		switch (type)
		{
			case 1:
				return type1;
			case 2:
				return type2;
		}
		return 0;
	}

	public bool DropInitBlock()
	{
		if(InitFishList.Count > 0)
		{
			GameObject block = Instantiate(m_blockPrefab, blockDropPos.position, blockDropPos.rotation) as GameObject;
			block.transform.SetParent(transform);
			IceBlock blockScript = block.GetComponent<IceBlock>();
			blockScript.SetType(InitFishList[0]);
			AddBlock(blockScript);
			InitFishList.RemoveAt(0);
			if (m_iceBlocks.Count >= 1)
				m_iceBlocks[0].m_isBottom = true;
			lastlastBlockType = lastBlockType;
			lastBlockType = blockScript.GetFishType();

			return false;
		}
		else
		{
			return true;
		}
		
	}

	public void DropBlock()
	{
		FishType type = GetRandomFishType();
		if(lastBlockType == lastlastBlockType)
		{
			if (type == FishType.RedFish)
				type = GetOneOfTwoFishType(FishType.GreenFish, FishType.BlueFish);
			if (type == FishType.GreenFish)
				type = GetOneOfTwoFishType(FishType.RedFish, FishType.BlueFish);
			if (type == FishType.BlueFish)
				type = GetOneOfTwoFishType(FishType.GreenFish, FishType.RedFish);
		}

		lastlastBlockType = lastBlockType;
		lastBlockType = type;
		GameObject block = Instantiate(m_blockPrefab, blockDropPos.position, blockDropPos.rotation) as GameObject;
		block.transform.SetParent(transform);

		IceBlock blockScript = block.GetComponent<IceBlock>();
		blockScript.SetType(type);
		//blockScript.SetRandomType();
		AddBlock(blockScript);
		CheckForMatch();
	}

    public void CheckForMatch()
    {
        FishType type = FishType.None;
        for (int i = 0; i < m_iceBlocks.Count; i++)
        {

            if (type == m_iceBlocks[i].GetFishType())
            {
                m_matchedBlocks.Add(m_iceBlocks[i]);
            }
            else
            {
                if (m_matchedBlocks.Count >= 3)
                {
                    break;
                }
                type = m_iceBlocks[i].GetFishType();

                m_matchedBlocks.Clear();
                m_matchedBlocks.Add(m_iceBlocks[i]);
            }
        }

        if (m_matchedBlocks.Count < 3)
        {
            m_matchedBlocks.Clear();
        }
        else
        {

			CombineMatchedBlock();

        }


        // Mark for matched
        foreach (IceBlock block in m_matchedBlocks)
        {
            block.m_isMatched = true;
        }





    }

    private void PlayMatchedEffect()
    {
        if (m_matchedBlocks.Count <= 0)
            Debug.LogError("matched Count must be more than one to run this function!");

    }


    private void CombineMatchedBlock()
    {

        if( m_matchedBlocks.Count <= 0 )
            Debug.LogError("matched Count must be more than one to run this function!");

        int insertIndex = m_iceBlocks.IndexOf(m_matchedBlocks[0]);
        FishType type = m_matchedBlocks[0].m_fish.m_foodType;
        Vector3 newPosition = Vector3.zero;
        {
            foreach (IceBlock block in m_matchedBlocks)
            {
                newPosition += block.transform.position;
            }
            newPosition /= m_matchedBlocks.Count;
        }
        
        foreach (IceBlock block in m_matchedBlocks)
        {
			DeleteBlock(block);
        }



        GameObject combinedBlock = Instantiate(m_blockPrefab) as GameObject;
        combinedBlock.transform.SetParent(transform);
        combinedBlock.transform.position = newPosition;

        IceBlock blockScript = combinedBlock.GetComponent<IceBlock>();
        blockScript.SetType(type);
        m_iceBlocks.Insert(insertIndex,blockScript);


        GameManager.GetInstance().m_specialEffectManager.PlayEffectAt(SpecialEffect.MatchedEffect,newPosition);

        m_matchedBlocks.Clear();



    }

	// This is called when the game is over
    public void DestroyLastFloor()
	{
		if(m_iceBlocks.Count > 0)
		{
			Destroy(m_iceBlocks[0].gameObject);
			Clear();

		}

	}



    


}
