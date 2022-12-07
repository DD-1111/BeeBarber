using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineCollider : MonoBehaviour
{
    int partNumber;

    public bool activeMode = true;
    [SerializeField]
    public GameObject prefabpart;

    public GameObject connectedbody;

    private List<Item> list;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        partNumber = transform.parent.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeMode) { 
            if (Physics.Linecast(transform.position, connectedbody.transform.position, LayerMask.GetMask("Saber")))
            {

                int ncut = int.Parse(transform.name) - 1;
                BattleManage.Instance.EnemeyTakeDamage(0.65f);
                Transform tmptrans = transform.parent.GetChild(ncut);
                GameObject dummy = Instantiate(prefabpart, new Vector3(tmptrans.position.x, tmptrans.position.y - 1.6f, tmptrans.position.z), Quaternion.identity, transform.parent.transform);
                tmptrans.GetComponent<ConfigurableJoint>().connectedBody = dummy.GetComponent<Rigidbody>();
                connectedbody = dummy;
                HairCutController hairCutController = dummy.GetComponent<HairCutController>();
                hairCutController.Record();
                dummy.GetComponent<Rigidbody>().AddExplosionForce(40, tmptrans.position, 20);
                // break joint of top joint, and initialized a dummy duplicated.
                for (int i = 0; i < ncut; i++)
                {
                    GameObject tmp = transform.parent.GetChild(i).gameObject;
                    SphereCollider sc = tmp.AddComponent<SphereCollider>() as SphereCollider;
                    sc.center = new Vector3(0f, 0f, 0f);
                    sc.radius = 0.1f;

                    if (i == 0)
                    {
                        tmp.GetComponent<Rigidbody>().AddExplosionForce(40, tmptrans.position, 20);
                    }

                    hairCutController = tmp.GetComponent<HairCutController>();
                    hairCutController.Record();
                   
                }
                
                if (ncut == 9)
                {
                    var temp = gameObject.GetComponent<Rigidbody>();
                    temp.isKinematic = false;
             

                }
                activeMode = false;
            }
            //activeMode = false;
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
