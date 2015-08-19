using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public float dropInterval = 2.5f;
	public float dropTime = -1;
    public Transform IceBlocks;


	bool isGamePlayed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isGamePlayed&&Time.time - dropTime > dropInterval)
		{
			bool hasDropedInitBlocks = GameManager.GetInstance().m_IceBlockManager.DropCachedBlock();
			//bool hasDropedInitBlocks = true;
			if (hasDropedInitBlocks  && GameManager.GetInstance().m_IceBlockManager.GetBlockCount() < 10)
			{
				GameManager.GetInstance().m_IceBlockManager.DropBlock();
			}
			dropTime = Time.time;

		}
	}

	public void PlayGame()
	{
		GameManager.GetInstance().m_IceBlockManager.InitBlocks(10, FishType.RedFish, FishType.GreenFish);
		//GameManager.GetInstance().m_IceBlockManager.DropBlock(IceBlockManager.GetRandomFishType());
		dropTime = Time.time;
		isGamePlayed = true;
	}

	public void RestartLevel()
	{
		GameManager.GetInstance().m_IceBlockManager.InitBlocks(10, FishType.RedFish, FishType.GreenFish);
		for (int i = 0; i < IceBlocks.childCount; i++)
		{
			Destroy(IceBlocks.GetChild(i).gameObject);
		}

		dropTime = Time.time;
		isGamePlayed = true;
	}

    //public void DropBlock()
    //{
    //    GameObject block = Instantiate(blockPrefab, blockDropPos.position, blockDropPos.rotation) as GameObject;
    //    block.transform.SetParent(IceBlocks);

    //    IceBlock blockScript = block.GetComponent<IceBlock>();
    //    blockScript.SetRandomType();

    //    IceBlockManager iceBlockManager = GameManager.GetInstance().m_IceBlockManager;
    //    iceBlockManager.AddBlock(blockScript);
    //    //iceBlockManager.CheckForMatch();
        


    //    GameManager.GetInstance().m_IceBlockManager.Clear();
    //    PlayGame();

    //}

	public void OnGameOver()
	{
		isGamePlayed = false;
	}


}
