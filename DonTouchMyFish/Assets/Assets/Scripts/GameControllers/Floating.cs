using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {
	Vector3 originPos;
	public float speed = 3f;
	public float  cycle= 8f;
	float LoopTime;
	// Use this for initialization
	void Start () {
		originPos = transform.position;
		LoopTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
		if (Time.time - LoopTime > cycle)
		{
			transform.position = originPos;
			LoopTime = Time.time;
		}
		
	}
}
