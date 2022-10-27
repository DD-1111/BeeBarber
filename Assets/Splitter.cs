using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR;

public class Splitter : MonoBehaviour
{
	public Material matCross;

    public Vector3 downPos;

    public Vector3 wordPosition;

    public GameObject cam;

    public GameObject player;

    public bool computationSaveMode;

    private bool hasMouseDown = false;

    Quaternion bladeRotation;

    public float cutThreshhold = 5f;

    private bool cut = false;

    private float vibeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float mx = Input.GetAxis("Mouse X");

        //transform.Rotate(0, 0, -mx);


        if (Input.GetMouseButtonDown(0) || OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.1f)
        {
            downPos = Input.mousePosition;
            hasMouseDown = true;
            downPos.z = Camera.main.nearClipPlane + 1;
        }
        //Debug.Log(cam.transform.forward);
        if (Input.GetMouseButtonUp(0) && hasMouseDown)
        {


            //Vector3 upPos = Input.mousePosition;
            //upPos.z = Camera.main.nearClipPlane + 1;
            //Vector3 mouseDiff = Camera.main.ScreenToWorldPoint(upPos) - Camera.main.ScreenToWorldPoint(downPos);
            //Vector3 centerPos = (Camera.main.ScreenToWorldPoint(upPos) + Camera.main.ScreenToWorldPoint(downPos)) / 2;
            //float planeLength = mouseDiff.magnitude;
            //float rotate = Mathf.Atan(mouseDiff.y / mouseDiff.x) / Mathf.PI * 180;
            //Debug.Log(rotate);

            //transform.position = centerPos;
            //transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            //float pointingX = cam.transform.forward.y * -69;
            // Debug.Log(pointingX);
            // Debug.Log(cam.transform.forward);
            //float pointingY = cam.transform.forward.z * -180;
            //transform.rotation = Quaternion.Euler(pointingX, pointingY, 0);
            Cut();
            hasMouseDown = false;
        }

        if (cut)
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            vibeTime += Time.deltaTime;
            if (vibeTime > 0.15f ){
                cut = false;
        
                vibeTime = 0f;
            }
        } else
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }


        Quaternion newbladeRotation = transform.parent.transform.parent.transform.rotation;

        if (!cut)
        {

            Vector3 dif = new Vector3(newbladeRotation.x - bladeRotation.x, newbladeRotation.y - bladeRotation.y, newbladeRotation.z - bladeRotation.z);
            float difsquare = Mathf.Abs(dif.y * 100);
            Debug.Log(difsquare);
            if (difsquare > cutThreshhold)
            {

                Cut();

            }
        }
 

        bladeRotation = newbladeRotation;
    }

    private void Cut()
    {
       
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale/2, transform.rotation, ~(LayerMask.GetMask("Solid")));
        foreach (Collider c in colliders)
        {

            // Add reactive force to the hair contacting blade, need to add explosion force to the part before and after this part.
            // unfinished yet

            //if (c.CompareTag("part"))
            //{
            //    Rigidbody temp = c.gameObject.GetComponent<Rigidbody>();
            //    temp.AddExplosionForce(100, c.gameObject.transform.position, 40, 2.0f);
            //}

            Destroy(c.gameObject);
            // GameObject[] objs = c.gameObject.SliceInstantiate(transform.position, transform.up 

            // only do full slice while computationSaveMode is off
            if (!c.CompareTag("part") || !computationSaveMode)
            {
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
                        rb.AddExplosionForce(100, c.gameObject.transform.position, 20);
                    }
                }
            }
            cut = true;
        }
    }
}
