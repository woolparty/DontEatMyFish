﻿using UnityEngine;
using System.Collections;

public class StartSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void JumpToGamePlayScene()
	{
		Application.LoadLevel("GamePlay");
	}
}
