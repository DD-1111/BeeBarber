using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCutController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dissapear;
    public bool isCut;
    public System.DateTime cutTime;
    private bool isKinematic;
    void Start()
    {
        isCut = false;
        isKinematic = false;
        cutTime = System.DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCut && !isKinematic)
        {
            if (cutTime.AddSeconds(10) < System.DateTime.Now)
            {
                isKinematic = true;
                transform.GetComponent<Rigidbody>().isKinematic = true;

                Destroy(transform.GetComponent<lineCollider>());
                Destroy(transform.GetComponent<SphereCollider>());
                Destroy(transform.GetComponent<GrappleRopeController>());
                cutTime = System.DateTime.Now;
            }
        }
        else if (isKinematic & dissapear)
        {
            if (cutTime.AddSeconds(5) < System.DateTime.Now)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }
}
