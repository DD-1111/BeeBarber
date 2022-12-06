using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class GrappleRopeController : MonoBehaviour
{

	private LineRenderer lineRenderer;
	private Transform connected;

	// Use this for initialization
	void Start()
	{
		lineRenderer = transform.GetComponent<LineRenderer>();
		connected = transform.GetComponent<lineCollider>().connectedbody.transform;
		lineRenderer.positionCount = 2;
	}

	// Update is called once per frame
	void Update()
	{
		connected = transform.GetComponent<lineCollider>().connectedbody.transform;
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, connected.position);
	}
}
