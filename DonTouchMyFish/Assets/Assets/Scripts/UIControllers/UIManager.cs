using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Text timeText;
	public StartPanel StartPanel;
	public InGamePanel InGamePanel;
	public GameOverPanel GameOverPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnGameStart()
	{
		StartPanel.Show();
		InGamePanel.Hide();
		GameOverPanel.Hide();
	}

	public void OnGamePlay()
	{
		StartPanel.Hide();
		InGamePanel.Show();
		GameOverPanel.Hide();
	}

	public void OnGameOver()
	{
		StartPanel.Hide();
		InGamePanel.Hide();
		GameOverPanel.Show();
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
