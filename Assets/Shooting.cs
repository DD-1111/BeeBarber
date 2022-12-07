using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject prefab;
    private float force = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
        obj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(100f, 100f, 100f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
