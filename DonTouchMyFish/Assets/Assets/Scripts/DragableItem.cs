using UnityEngine;
using System.Collections;

public class DragableItem : MonoBehaviour {

	private Vector3 dist;
	private float posX;
	private float posY;

	private bool isDragging;

	void Start()
	{
		isDragging = false;
	}


	void Update()
	{
		if(isDragging)
		{
			//rigidbody.angularVelocity = Vector3.zero;
			//transform.localRotation = Quaternion.identity;
		}
        

	   // Vector3 position = transform.localPosition;
       // transform.localPosition = new Vector3(position.x, position.y,0);
		//transform.localRotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z);
	}


	void OnMouseDown()
	{
		isDragging = true;

		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;

	}

	void OnMouseDrag()
	{
		Vector3 curPos = new Vector3(Input.mousePosition.x - posX, dist.y, dist.z);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);

	    float diff = (worldPos - transform.position).x;
        rigidbody.velocity = new Vector3(diff / Time.deltaTime, rigidbody.velocity.y, 0);
	}

	void OnMouseUp()
	{
		isDragging = false;

	}
}
