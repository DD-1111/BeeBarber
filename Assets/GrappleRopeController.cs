using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class GrappleRopeController : MonoBehaviour
{

	private LineRenderer lineRenderer;
	private GameObject connected;

	// Use this for initialization
	void Start()
	{
		lineRenderer = transform.GetComponent<LineRenderer>();
		connected = transform.GetComponent<SpringJoint>().connectedBody.gameObject;
		lineRenderer.positionCount = 2;
	}

	// Update is called once per frame
	void Update()
	{
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, connected.transform.position);
	}
}
