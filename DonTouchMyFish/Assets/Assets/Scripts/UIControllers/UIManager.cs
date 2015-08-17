using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Text timeText;
	public InGamePanel InGamePanel;
	public GameOverPanel gameOverPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnGameStart()
	{
		InGamePanel.Hide();
		gameOverPanel.Hide();
	}

	public void OnGamePlay()
	{
		InGamePanel.Show();
		gameOverPanel.Hide();
	}

	public void OnGameOver()
	{
		InGamePanel.Hide();
		gameOverPanel.Show();
	}


	public void SetScore(int score)
	{
		scoreText.text = "Score: " + score;
	}

	public void SetTime(float score)
	{
		double s = System.Math.Round(score, 1);
		if(s%1 == 0)
			timeText.text = "Time: " + s + ".0";
		else
			timeText.text = "Time: " + System.Math.Round(score, 1);
	}

	
}
