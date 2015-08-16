using UnityEngine;
using System.Collections;

public class AngerBar : MonoBehaviour {
	public GameObject Anger;
	float height = 0f;
	// Use this for initialization
	void Start () {
		height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			Anger.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, transform.GetComponent<RectTransform>().anchoredPosition.y + height * 0.2f);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, transform.GetComponent<RectTransform>().anchoredPosition.y + height * 0.2f);
			SetColor(Color.blue);
		}
	}

	public void SetColor(Color color)
	{
		Anger.GetComponent<UnityEngine.UI.Image>().color = color;
	}

	public void SetAngerCount(int count)
	{
		Anger.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, height * count * 0.01f);
	}

	
}
