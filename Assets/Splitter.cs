using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Splitter : MonoBehaviour
{
	public Material matCross;
    public Vector3 downPos;
    public Vector3 wordPosition;
    public GameObject cam;
    public GameObject player;
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
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            downPos = Input.mousePosition;
            hasMouseDown = true;
            downPos.z = Camera.main.nearClipPlane + 1;
        }
        Debug.Log(cam.transform.forward);
        if (Input.GetMouseButtonUp(0) && hasMouseDown)
        {
            Vector3 upPos = Input.mousePosition;
            upPos.z = Camera.main.nearClipPlane + 1;
            Vector3 mouseDiff = Camera.main.ScreenToWorldPoint(upPos) - Camera.main.ScreenToWorldPoint(downPos);
            //Vector3 centerPos = (Camera.main.ScreenToWorldPoint(upPos) + Camera.main.ScreenToWorldPoint(downPos)) / 2;
            float planeLength = mouseDiff.magnitude;
            float rotate = Mathf.Atan(mouseDiff.y / mouseDiff.x) / Mathf.PI * 180;
            Debug.Log(rotate);

            //transform.position = centerPos;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            float pointingX = cam.transform.forward.y * -69;
            Debug.Log(pointingX);
            Debug.Log(cam.transform.forward);
            float pointingY = cam.transform.forward.z * -180;
            //transform.rotation = Quaternion.Euler(pointingX, pointingY, 0);

            Cut();
            hasMouseDown = false;
        }
    }

    private void Cut()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale/2, transform.rotation, ~LayerMask.GetMask("Solid"));
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