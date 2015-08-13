using UnityEngine;
using System.Collections;

public class DragableItem : MonoBehaviour {

	private Vector3 m_dist;
    private float m_posX;
    private float m_posY;

    private bool m_isDragging;

    public IceBlock m_iceblock;

	void Start()
	{
        m_isDragging = false;
	}


	void Update()
	{
        if (m_isDragging)
		{
			//rigidbody.angularVelocity = Vector3.zero;
			//transform.localRotation = Quaternion.identity;
            
		}
        m_iceblock.CheckForDestroy();

	   // Vector3 position = transform.localPosition;
       // transform.localPosition = new Vector3(position.x, position.y,0);
		//transform.localRotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z);
	}

    #region drag callbacks 
    void OnMouseDown()
	{
        m_isDragging = true;

        m_dist = Camera.main.WorldToScreenPoint(transform.position);
        m_posX = Input.mousePosition.x - m_dist.x;
        m_posY = Input.mousePosition.y - m_dist.y;

	}

	void OnMouseDrag()
	{
        Vector3 curPos = new Vector3(Input.mousePosition.x - m_posX, m_dist.y, m_dist.z);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);

	    float diff = (worldPos - transform.position).x;
        rigidbody.velocity = new Vector3(diff / Time.deltaTime, rigidbody.velocity.y, 0);
	}

	void OnMouseUp()
	{
        m_isDragging = false;

    }
    #endregion
}
