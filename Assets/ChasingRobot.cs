using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingRobot : MonoBehaviour
{

    public Transform target;
    public GameObject prefab;
    public float deltaTime = 3f;
    public float force = 1200;
    public int damping = 7;

    public int lowestHeight = 12;
    public int minimumDis = 6;
    public Transform bulletpoint;

    public float speed = 2f;
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

            if (Vector3.Distance(transform.position, target.position) < minimumDis)
            {
                return;
            }
            var step = speed * Time.deltaTime;
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, step);
            temp.y = Mathf.Max(lowestHeight, temp.y);
            transform.position = temp;

         
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
       
        rb.AddForce(force * (1f + 0.8f * Random.value) * transform.forward);
    }

}
