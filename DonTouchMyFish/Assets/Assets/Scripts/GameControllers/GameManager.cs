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
	public UIManager m_UIManager;
	public IceBlockManager m_IceBlockManager;
	int score = 0;
	public UnityEngine.UI.Text scoreText;
	float OneRoundTime = 60.0f;

	void Awake()
	{
		instance = this;
		StartGame();
		//ResetGame();
	}

	void initGame()
	{

	}

	public void StartGame()
	{
		state = GameStatus.Begin;
		m_UIManager.OnGameStart();
	}

	public void PlayGame()
	{
		state = GameStatus.Started;
		m_UIManager.OnGamePlay();
		m_UIManager.SetScore(0);
		OneRoundTime = 60.0f;
		m_UIManager.SetTime(OneRoundTime);
		m_LevelController.PlayGame();
	}

	// Update is called once per frame
	void Update()
	{
		if(state == GameStatus.Started)
		{
			OneRoundTime -= Time.deltaTime;
			if(OneRoundTime < 0)
			{
				GameOver();
			}
			else
			{
				m_UIManager.SetTime(OneRoundTime);
			}
		}
	}

	public void RestartGame()
	{
		score = 0;
		m_LevelController.RestartLevel();
		OneRoundTime = 60.0f;
		m_UIManager.SetTime(OneRoundTime);
		m_UIManager.OnGamePlay();
		state = GameStatus.Started;
	}

	public void AddScore(int _score)
	{
		score += _score;
		m_UIManager.SetScore(score);
	}


	public void GameOver()
	{
		state = GameStatus.End;
		m_LevelController.OnGameOver();
		m_UIManager.OnGameOver();
		m_IceBlockManager.Clear();
	}
}
