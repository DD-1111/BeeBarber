using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineCollider : MonoBehaviour
{
    int partNumber;
    HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("Healthbar");
        healthBar = g.GetComponent<HealthBar>();
        partNumber = transform.parent.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Linecast(transform.position, transform.GetComponent<SpringJoint>().connectedBody.transform.position, LayerMask.GetMask("Solid")))
        {
            int ncut = int.Parse(transform.name) - 1;
            healthBar.TakeDamage(0.035f);
            Destroy(transform.parent.GetChild(ncut).GetComponent<SpringJoint>());
            Destroy(transform.parent.GetChild(ncut).GetComponent<LineRenderer>());
            Destroy(transform.parent.GetChild(ncut).GetComponent<lineCollider>());
            Destroy(transform.parent.GetChild(ncut).GetComponent<GrappleRopeController>());
            for (int i = 0; i < ncut; i++)
            {
                GameObject tmp = transform.parent.GetChild(i).gameObject;
                SphereCollider sc = tmp.AddComponent<SphereCollider>() as SphereCollider;
                sc.center = new Vector3(0f, 0f, 0f);
                sc.radius = 0.1f;
            }
        }
    }
}
