using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class GrappleRopeController : MonoBehaviour
{

	public LineRenderer lineRenderer;

	// Use this for initialization
	void Start()
	{
		lineRenderer.positionCount = transform.parent.childCount;
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < lineRenderer.positionCount; ++i)
		{
			lineRenderer.SetPosition(i, transform.parent.GetChild(i).position);
		}
	}
}
