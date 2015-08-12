using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IceBlockManager : MonoBehaviour
{

    private List<IceBlock> m_iceBlocks;
    private List<IceBlock> m_matchedBlocks; 

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
	}

    public void AddBlock(IceBlock i_iceblock)
    {
        m_iceBlocks.Add(i_iceblock);
		FishType type = m_iceBlocks[m_iceBlocks.Count - 1].foodType;
        i_iceblock.transform.parent = transform;
    }

	public void DeleteBlock(IceBlock i_iceblock)
	{
		m_iceBlocks.Remove(i_iceblock);
		Destroy(i_iceblock.gameObject);
	}

    public void CheckForMatch()
    {
        FishType type = FishType.None;
        for (int i = 0; i < m_iceBlocks.Count; i++)
        {


            if (type == m_iceBlocks[i].foodType)
            {

                m_matchedBlocks.Add(m_iceBlocks[i]);
            }
            else
            {
                if (m_matchedBlocks.Count >= 3)
                {
                    break;
                }


                type = m_iceBlocks[i].foodType;

                m_matchedBlocks.Clear();
                m_matchedBlocks.Add(m_iceBlocks[i]);
            }
        }

        if (m_matchedBlocks.Count < 3)
        {
            m_matchedBlocks.Clear();
        }

        foreach (IceBlock block in m_matchedBlocks)
        {
            m_iceBlocks.Remove(block);
            Destroy(block.gameObject);
        }

        m_matchedBlocks.Clear();
    }

}
