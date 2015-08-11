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

    public LevelController m_LevelController;


    // Use this for initialization
	void Awake ()
	{
	    instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
