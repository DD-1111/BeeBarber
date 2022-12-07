using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingRobot : MonoBehaviour
{

    public Transform target;
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) < 0.05f)
            {
                return;
            }
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }
}
