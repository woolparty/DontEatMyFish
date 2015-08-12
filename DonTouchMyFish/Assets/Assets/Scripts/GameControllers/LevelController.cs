using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public GameObject blockPrefab;
	public Transform IceBlocks;
	public Transform blockDropPos;
	public float dropInterval = 2.5f;
	public float dropTime = -1;
	bool isGamePlayed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isGamePlayed && Time.time - dropTime > dropInterval)
		{
			DropBlock();
			dropTime = Time.time;
		}
	}

	public void PlayGame()
	{
		DropBlock();
		dropTime = Time.time;
		isGamePlayed = true;
	}

	public void RestartLevel()
	{
		for (int i = 0; i < IceBlocks.childCount; i++)
		{
			Destroy(IceBlocks.GetChild(i).gameObject);
		}
		DropBlock();
		dropTime = Time.time;
		isGamePlayed = true;
	}

	public void DropBlock()
	{
		GameObject block = Instantiate(blockPrefab, blockDropPos.position, blockDropPos.rotation) as GameObject;
		block.transform.SetParent(IceBlocks);
		block.GetComponent<IceBlock>().SetRandomType();
	}

	public void OnGameOver()
	{
		isGamePlayed = false;
	}


}
