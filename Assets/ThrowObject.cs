using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
	public List<GameObject> prefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
        	GameObject prefab = prefabs[Random.Range(0, prefabs.Count - 1)];
        	GameObject go = Instantiate(prefab, transform.position, transform.rotation);

        	Rigidbody rigid = go.GetComponent<Rigidbody>();
        	rigid.AddForce(Vector3.up * 300);
        }
    }
}
