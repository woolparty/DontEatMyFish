using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IceBlockManager : MonoBehaviour
{

    private List<IceBlock> m_iceBlocks;
	private List<List<IceBlock>> m_iceBlockGroup;

    private List<List<IceBlock>> m_matchedBlockQueue;
    private float m_Counter;

    
    public  GameObject     m_blockPrefab;

	public Transform blockDropPos;
	List<FishType> InitFishList = new List<FishType>();
	FishType lastBlockType;
	FishType lastlastBlockType;


	// Use this for initialization
	void Awake ()
	{
	    m_Counter = -1f;
        m_matchedBlockQueue = new List<List<IceBlock>>();
	    m_iceBlocks = new List<IceBlock>();

		m_iceBlockGroup = new List<List<IceBlock>>();
	}
	private const float minDistanceX = 2.2f;
	private const float minDistanceY = 2.1f;
	private void FindBlockGroups()
	{
		if(m_iceBlocks.Count == 0)
			return;

		m_iceBlockGroup = new List<List<IceBlock>>();
		List<IceBlock> group = new List<IceBlock>();
		group.Add(m_iceBlocks[0]);

		for(int i = 1; i < m_iceBlocks.Count ; i++)
		{
			if( !IsConnected(m_iceBlocks[i-1],m_iceBlocks[i]) )
			{
				m_iceBlockGroup.Add(group);
				group = new List<IceBlock>();
			}
			group.Add(m_iceBlocks[i]);

		}
		m_iceBlockGroup.Add(group);
	}
	private bool IsConnected(IceBlock i_A,IceBlock i_B)
	{
		return ( Mathf.Abs(i_A.transform.position.y-i_B.transform.position.y) <= minDistanceY 
		        && Mathf.Abs(i_A.transform.position.x-i_B.transform.position.x) <= minDistanceX );

	}
	private void CheckForMatchedByGroup()
	{
		foreach( List<IceBlock> list in m_iceBlockGroup )
		{
			CheckForMatch(list);
		}
	}
	public void CheckForMatch(List<IceBlock> i_list)
	{
		FishType type = FishType.None;
		List<IceBlock> temp = new List<IceBlock>();
		for (int i = 0; i < i_list.Count; i++)
		{
			
			if (i_list[i].m_isMatched)// || m_iceBlocks[i].rigidbody.velocity.magnitude > 0.5f)
			{
				type = FishType.None;
			}
			
			if (type == i_list[i].GetFishType())
			{
				temp.Add(i_list[i]);
			}
			else
			{
				if (temp.Count >= 3)
				{
					
					m_matchedBlockQueue.Add(temp);
					foreach (IceBlock block in temp)
					{
						block.m_isMatched = true;
						block.Flash();
					}
					
				}
				type = i_list[i].GetFishType();
				
				temp = new List<IceBlock>();
				temp.Add(i_list[i]);
			}
		}
		
		if (temp.Count >= 3)
		{
			
			m_matchedBlockQueue.Add(temp);
			foreach (IceBlock block in temp)
			{
				block.m_isMatched = true;
				block.Flash();
			}
		}
		
		
		if(m_matchedBlockQueue.Count > 0)
		{
			if(m_Counter <= 0.0f)
				m_Counter = 0.3f;
		}
		
	}

	private void AutoAligning()
	{

		float x = 0;
		foreach( Transform trans in transform )
		{
			x += trans.position.x;
		}
		x /= transform.childCount;

		float rotation = 0;
		float newX = 0;
		foreach( Transform trans in transform )
		{
			if( trans.rotation.eulerAngles.z < 30 || trans.rotation.eulerAngles.z > 330 )
			{
				newX = Mathf.Lerp(trans.position.x,x,0.05f);
				trans.position = new Vector3(newX,trans.position.y,trans.position.z);
			}

		}

	}
	void ReCheckForMatchedGroup()
	{

	}

	int lastCount = 0;
	// Update is called once per frame
	void Update ()
	{
        if (m_iceBlocks.Count >= 1)
            m_iceBlocks[0].m_isBottom = true;

		FindBlockGroups();
		if( lastCount != m_iceBlockGroup.Count )
			UpdateMatchedBlocks();
		lastCount = m_iceBlockGroup.Count;

		CheckForMatchedByGroup();

	    if (m_Counter < 0)
	        return;
		AutoAligning();
	    m_Counter -= Time.deltaTime;
	    if (m_Counter <= 0.0f)
	    {
            CombineMatchedBlock();

	    }


	}


	public int GetBlockCount()
	{
		return m_iceBlocks.Count;
	}

	float offset = 0.0f;
    public void AddBlock(IceBlock i_iceblock)
    {
        m_iceBlocks.Add(i_iceblock);
        i_iceblock.transform.parent = transform;

		//UpdateMatchedBlocks();

		i_iceblock.transform.position += new Vector3(0,0,offset);
		offset -= 0.0001f;
    }

	public void DeleteBlock(IceBlock i_iceblock)
	{
		m_iceBlocks.Remove(i_iceblock);
		Destroy(i_iceblock.gameObject);

        if (m_iceBlocks.Count >= 1)
            m_iceBlocks[0].m_isBottom = true;

		//UpdateMatchedBlocks();
	}
	
	public void Clear()
	{
		m_iceBlocks.Clear();
		//m_matchedBlocks.Clear();
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
		}
		else
		{
			//DropBlock(type2);
			InitFishList.Add(type2);
			lastlastBlockType = type2;
		}

		random = Random.Range(0, 0.999f);
		if (random > 0.5f)
		{
			//DropBlock(type1);
			InitFishList.Add(type1);
			lastBlockType = type1;
		}
		else
		{
			//DropBlock(type2);
			InitFishList.Add(type2);
			lastBlockType = type2;
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
				}
				else
				{
					//DropBlock(type1);
					InitFishList.Add(type1);
					lastBlockType = type1;
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
				}
				else
				{
					//DropBlock(type2);
					InitFishList.Add(type2);
					lastlastBlockType = lastBlockType;
					lastBlockType = type2;
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

	public bool DropCachedBlock()
	{
		DropBlocks( InitFishList);
		return true;
		/*if(InitFishList.Count > 0)
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
		}*/
		
	}

	public void DropBlocks(List<FishType> typeList)
	{
		if(InitFishList.Count == 0)
		{
			return;
		}

		float deltaY = 3f;
		GameObject block = new GameObject();
		for(int i = 0; i< typeList.Count; i++)
		{
			if(m_iceBlocks.Count > 0)
				block = Instantiate(m_blockPrefab, new Vector3(m_iceBlocks[m_iceBlocks.Count - 1].transform.position.x, blockDropPos.position.y + i * deltaY, blockDropPos.position.z), blockDropPos.rotation) as GameObject;
			else
				block = Instantiate(m_blockPrefab, new Vector3(blockDropPos.transform.position.x, blockDropPos.position.y + i * deltaY, blockDropPos.position.z), blockDropPos.rotation) as GameObject;


			block.transform.SetParent(transform);
			IceBlock blockScript = block.GetComponent<IceBlock>();
			blockScript.SetType(typeList[i]);
			AddBlock(blockScript);
			lastlastBlockType = lastBlockType;
			lastBlockType = blockScript.GetFishType();
		}
		if (m_iceBlocks.Count >= 1)
			m_iceBlocks[0].m_isBottom = true;

		InitFishList.Clear();
	}

	public void DropBlock()
	{
		FishType type = GetRandomFishType();
		if(lastBlockType == lastlastBlockType)
		{
			if (lastBlockType == FishType.RedFish)
				type = GetOneOfTwoFishType(FishType.GreenFish, FishType.BlueFish);
			if (lastBlockType == FishType.GreenFish)
				type = GetOneOfTwoFishType(FishType.RedFish, FishType.BlueFish);
			if (lastBlockType == FishType.BlueFish)
				type = GetOneOfTwoFishType(FishType.GreenFish, FishType.RedFish);
		}

		lastlastBlockType = lastBlockType;
		lastBlockType = type;
		GameObject block = new GameObject();
		if (m_iceBlocks.Count > 0)
			block = Instantiate(m_blockPrefab, new Vector3(m_iceBlocks[m_iceBlocks.Count - 1].transform.position.x, blockDropPos.position.y, blockDropPos.position.z), blockDropPos.rotation) as GameObject;
		else
			block = Instantiate(m_blockPrefab, new Vector3(blockDropPos.transform.position.x, blockDropPos.position.y, blockDropPos.position.z), blockDropPos.rotation) as GameObject;
		//GameObject block = Instantiate(m_blockPrefab, blockDropPos.position, blockDropPos.rotation) as GameObject;
		block.transform.SetParent(transform);

		IceBlock blockScript = block.GetComponent<IceBlock>();
		blockScript.SetType(type);
		//blockScript.SetRandomType();
		AddBlock(blockScript);
		//CheckForMatch();
	}

	public void UpdateMatchedBlocks()
	{
		foreach(IceBlock iceBlock in m_iceBlocks)
		{
			iceBlock.m_isMatched = false;
		}
		m_matchedBlockQueue = new List<List<IceBlock>>();

		m_Counter = -1.0f;
	}


    private void PlayMatchedEffect()
    {

    }


    private void CombineMatchedBlock()
    {


        foreach (List<IceBlock> matchedBlocks in m_matchedBlockQueue)
        {


            int insertIndex = m_iceBlocks.IndexOf(matchedBlocks[0]);
            FishType type = matchedBlocks[0].m_fish.m_foodType;
            Vector3 newPosition = Vector3.zero;
            {
                foreach (IceBlock block in matchedBlocks)
                {
                    newPosition += block.transform.position;
                }
                newPosition /= matchedBlocks.Count;
            }

            foreach (IceBlock block in matchedBlocks)
            {
                DeleteBlock(block);
            }
			FishType firstType = FishType.RedFish;
			FishType secondType = FishType.GreenFish;

			RankFishType(m_iceBlocks, out firstType, out secondType);

			GameManager.GetInstance().m_IceBlockManager.InitBlocks(matchedBlocks.Count, firstType, secondType);
			Vector3 pos = Vector3.zero;


//            GameObject combinedBlock = Instantiate(m_blockPrefab) as GameObject;
//            combinedBlock.transform.SetParent(transform);
//            combinedBlock.transform.position = newPosition;
//
//            IceBlock blockScript = combinedBlock.GetComponent<IceBlock>();
//            blockScript.SetType(type);
//            m_iceBlocks.Insert(insertIndex, blockScript);


            GameManager.GetInstance().m_specialEffectManager.PlayEffectAt(SpecialEffect.MatchedEffect, newPosition);

            matchedBlocks.Clear();

        }


        m_matchedBlockQueue.Clear();

		UpdateMatchedBlocks();


    }

	void RankFishType(List<IceBlock> iceblocks, out FishType RankFirstType, out FishType RankSecondType)
	{
		int RedCount = 0, GreenCount = 0, BlueCount = 0;
		for (int i = 0; i < iceblocks.Count; i++)
		{
			switch (iceblocks[i].GetFishType())
			{
				case FishType.RedFish:
					++RedCount;
					break;
				case FishType.GreenFish:
					++GreenCount;
					break;
				case FishType.BlueFish:
					++BlueCount;
					break;
			}
		}


		if(RedCount>=GreenCount)
		{
			if(GreenCount>=BlueCount)
			{
				RankFirstType = FishType.RedFish;
				RankSecondType = FishType.GreenFish;
			}else
			{
				if (RedCount >= BlueCount)
				{
					RankFirstType = FishType.RedFish;
					RankSecondType = FishType.BlueFish;
				}
				else
				{
					RankFirstType = FishType.BlueFish;
					RankSecondType = FishType.RedFish;
				}
			}
		}
		else
		{
			if (GreenCount >= BlueCount)
			{
				RankFirstType = FishType.GreenFish;
				if(BlueCount >= RedCount)
					RankSecondType = FishType.BlueFish;
				else
				{
					RankSecondType = FishType.RedFish;
				}
			}
			else
			{
				RankFirstType = FishType.BlueFish;
				RankSecondType = FishType.GreenFish;
			}
		}
	}


	// This is called when the game is over
    public void DestroyLastFloor()
	{
		if(m_iceBlocks.Count > 0)
		{
			Destroy(m_iceBlocks[0].gameObject);
            m_iceBlocks.Clear();


		}

	}



    


}
