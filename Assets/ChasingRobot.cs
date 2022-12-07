using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingRobot : MonoBehaviour
{

    public Transform target;
    private float speed = 0.8f;
    private int damping = 2;
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
            if (Vector3.Distance(transform.position, target.position) < 0.05f)
            {
                return;
            }
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }

    private void UpdateRotation() {
        var lookPos = target.position - transform.position;
        //lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

}
