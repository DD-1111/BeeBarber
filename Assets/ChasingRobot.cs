using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingRobot : MonoBehaviour
{

    public Transform target;
    public GameObject prefab;
    public float deltaTime = 2f;
    public float force = 1000;
    public int damping = 5;

    public Transform bulletpoint;

    private float speed = 0.8f;
    private float seconds = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            UpdateRotation();
            //Debug.Log("rotation " + transform.rotation);
            if (Vector3.Distance(transform.position, target.position) < 0.05f)
            {
                return;
            }
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            seconds += Time.deltaTime;
            if (seconds >= deltaTime)
            {
                ThrowObject();
                seconds = 0;
            }
        }

    }

    private void UpdateRotation() {
        var lookPos = target.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    private void ThrowObject()
    {
        GameObject obj = Instantiate(prefab, bulletpoint.position, transform.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        //rb.AddRelativeForce(force * new Vector3(0, 0, 1));
        rb.AddForce(force * transform.forward);
    }

}
