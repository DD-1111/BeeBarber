using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineCollider : MonoBehaviour
{
    int partNumber;
    HealthBar healthBar;
    public bool activeMode = true;
    [SerializeField]
    public GameObject prefabpart;

    public GameObject connectedbody;

    private List<Item> list;
    private IEnumerator coroutine;
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
        if (activeMode) { 
            if (Physics.Linecast(transform.position, connectedbody.transform.position, LayerMask.GetMask("Solid")))
            {
                int ncut = int.Parse(transform.name) - 1;
                healthBar.TakeDamage(0.035f);
                Transform tmptrans = transform.parent.GetChild(ncut);
                GameObject tmp;
                tmp = Instantiate(prefabpart, new Vector3(tmptrans.position.x, tmptrans.position.y - 1.6f, tmptrans.position.z), Quaternion.identity, transform.parent.transform);
                tmptrans.GetComponent<ConfigurableJoint>().connectedBody = tmp.GetComponent<Rigidbody>();
                connectedbody = tmp;
                HairCutController hairCutController = tmp.GetComponent<HairCutController>();
                hairCutController.isCut = true;
                hairCutController.cutTime = System.DateTime.Now; 
                // break joint of top joint, and initialized a dummy duplicated.
                for (int i = 0; i < ncut; i++)
                {
                    tmp = transform.parent.GetChild(i).gameObject;
                    SphereCollider sc = tmp.AddComponent<SphereCollider>() as SphereCollider;
                    sc.center = new Vector3(0f, 0f, 0f);
                    sc.radius = 0.1f;

                    hairCutController = tmp.GetComponent<HairCutController>();
                    hairCutController.isCut = true;
                    hairCutController.cutTime = System.DateTime.Now;
                }
                activeMode = false;
            }
        }
    }
}

public class Item
{
    public GameObject gameObject;
    public System.DateTime time;

    public Item(GameObject gameObject, System.DateTime time)
    {
        this.gameObject = gameObject;
        this.time = time;
    }

}
