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

	public GameStatus state = GameStatus.Begin;
	public LevelController m_LevelController;
	public UIManager m_UIManager;
	public IceBlockManager m_IceBlockManager;
    public SpecialEffectManager m_specialEffectManager;
	int score = 0;
	public UnityEngine.UI.Text scoreText;
	float OneRoundTime = 60.0f;

	public GameObject PenguinLeft;
	public GameObject PenguinRight;

	void Awake()
	{
		instance = this;
		StartGame();
		PlayGame();
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
		OneRoundTime = 60.0f;
		m_UIManager.SetTime(OneRoundTime);
		m_UIManager.SetScore(score);
		m_UIManager.OnGamePlay();
		m_LevelController.RestartLevel();
		m_IceBlockManager.Clear();
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

	}
}
