using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Splitter : MonoBehaviour
{
	public Material matCross;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");

        transform.Rotate(0, 0, -mx);

        if (Input.GetMouseButtonDown(0))
        {
        	Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(4, 0.005f, 4), transform.rotation, ~LayerMask.GetMask("Solid"));

        	foreach (Collider c in colliders)
        	{
        		Destroy(c.gameObject);
        		// GameObject[] objs = c.gameObject.SliceInstantiate(transform.position, transform.up 

        		SlicedHull hull = c.gameObject.Slice(transform.position, transform.up);
        		if (hull != null)
        		{
        			GameObject lower = hull.CreateLowerHull(c.gameObject, matCross);
        			GameObject upper = hull.CreateUpperHull(c.gameObject, matCross);
        			GameObject[] objs = new GameObject[] {lower, upper};

        			foreach (GameObject obj in objs)
        			{
        				Rigidbody rb = obj.AddComponent<Rigidbody>();
        				obj.AddComponent<MeshCollider>().convex = true;
        				rb.AddExplosionForce(100, c.gameObject.transform.position, 20);
        			}
        		}
        	}
        }
    }
}
