using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IceBlockManager : MonoBehaviour
{

    private List<IceBlock> m_iceBlocks;
    private List<IceBlock> m_matchedBlocks;
    
    public  GameObject     m_blockPrefab;

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
	}

	public void InitBlocks(int count)
	{
		for(int i = 0; i< count; i++)
		{

		}
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


        // Combine if find any matches
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
