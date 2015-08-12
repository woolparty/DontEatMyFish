using UnityEngine;
using System.Collections;

public class InGamePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Show()
	{
		GetComponent<CanvasGroup>().alpha = 1;
		GetComponent<CanvasGroup>().interactable = true;
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		this.gameObject.SetActive(true);
	}

	public void Hide()
	{
		GetComponent<CanvasGroup>().alpha = 0;
		GetComponent<CanvasGroup>().interactable = false;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		this.gameObject.SetActive(false);
	}
}
