using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	#region Singleton
	private static GameManager instance;
	public static GameManager GetInstance()
	{
		if (instance == null)
			Debug.LogError("No GameManager!!!");
		return instance;
	}
	#endregion

	GameStatus state = GameStatus.Begin;
	public LevelController m_LevelController;
	int score = 0;
	public UnityEngine.UI.Text scoreText;
	public Transform blockDropPos;
	public float dropInterval = 2.5f;
	public float dropTime = -1;
	public GameObject blockPrefab;

   //CSY
    public IceBlockManager m_IceBlockManager;

	void Awake()
	{
		instance = this;
		ResetGame();
	}

	public void StartGame()
	{
		state = GameStatus.Started;
		DropBlock();
		dropTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time - dropTime > dropInterval)
		{
			DropBlock();
			dropTime = Time.time;
		}
	}

	public void ResetGame()
	{
		score = 0;
	}

	public void AddScore(int _score)
	{
		score += _score;
		scoreText.text = "Score: " + score;
	}

	public void DropBlock()
	{
		GameObject block = Instantiate(blockPrefab, blockDropPos.position, blockDropPos.rotation) as GameObject;
	    IceBlock blockScript = block.GetComponent<IceBlock>();
        blockScript.SetRandomType();

        //CSY
        m_IceBlockManager.AddBlock(blockScript);
	}
}
