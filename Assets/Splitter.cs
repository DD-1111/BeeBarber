using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Splitter : MonoBehaviour
{
	public Material matCross;
    public Vector3 downPos;
    public Vector3 wordPosition;
    private bool hasMouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float mx = Input.GetAxis("Mouse X");

        //transform.Rotate(0, 0, -mx);


        if (Input.GetMouseButtonDown(0))
        {
            downPos = Input.mousePosition;
            hasMouseDown = true;
            downPos.z = Camera.main.nearClipPlane + 1;
        }

        if (Input.GetMouseButtonUp(0) && hasMouseDown)
        {
            Vector3 upPos = Input.mousePosition;
            upPos.z = Camera.main.nearClipPlane + 1;
            Vector3 mouseDiff = Camera.main.ScreenToWorldPoint(upPos) - Camera.main.ScreenToWorldPoint(downPos);
            Debug.Log(mouseDiff);
            Vector3 centerPos = (Camera.main.ScreenToWorldPoint(upPos) + Camera.main.ScreenToWorldPoint(downPos)) / 2;
            transform.position = centerPos;
            float planeLength = mouseDiff.magnitude;
            transform.localScale = new Vector3(planeLength, transform.localScale.y, transform.localScale.z);
            float rotate = Mathf.Atan(mouseDiff.y / mouseDiff.x) / Mathf.PI * 180;
            Debug.Log(rotate);
            transform.rotation = Quaternion.Euler(0, 0, rotate);
            Cut();
            hasMouseDown = false;
        }
    }

    private void Cut()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale/2, transform.rotation, ~LayerMask.GetMask("Solid"));
        Debug.Log(colliders.Length);
        foreach (Collider c in colliders)
        {
            Destroy(c.gameObject);
            // GameObject[] objs = c.gameObject.SliceInstantiate(transform.position, transform.up 

            SlicedHull hull = c.gameObject.Slice(transform.position, transform.up);
            if (hull != null)
            {
                GameObject lower = hull.CreateLowerHull(c.gameObject, matCross);
                GameObject upper = hull.CreateUpperHull(c.gameObject, matCross);
                GameObject[] objs = new GameObject[] { lower, upper };

                foreach (GameObject obj in objs)
                {
                    Rigidbody rb = obj.AddComponent<Rigidbody>();
                    obj.AddComponent<MeshCollider>().convex = true;
                    obj.transform.parent = GameObject.Find("CutObjects").transform;
                    rb.AddExplosionForce(100, c.gameObject.transform.position, 20);
                }
            }
        }
    }
}
