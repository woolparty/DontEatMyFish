using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public float dropInterval = 2.5f;
	public float dropTime = -1;
	bool isGamePlayed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isGamePlayed && Time.time - dropTime > dropInterval && GameManager.GetInstance().m_IceBlockManager.GetBlockCount() < 11)
		{
			GameManager.GetInstance().m_IceBlockManager.DropBlock(IceBlockManager.GetRandomFishType());
			dropTime = Time.time;
		}
	}

	public void PlayGame()
	{
		//GameManager.GetInstance().m_IceBlockManager.InitBlocks(10, FishType.RedFish, FishType.GreenFish);
		GameManager.GetInstance().m_IceBlockManager.DropBlock(IceBlockManager.GetRandomFishType());
		dropTime = Time.time;
		isGamePlayed = true;
	}

	public void RestartLevel()
	{
		GameManager.GetInstance().m_IceBlockManager.Clear();
		GameManager.GetInstance().m_IceBlockManager.DropBlock(IceBlockManager.GetRandomFishType());
		dropTime = Time.time;
		isGamePlayed = true;
	}

	public void OnGameOver()
	{
		isGamePlayed = false;
	}


}
