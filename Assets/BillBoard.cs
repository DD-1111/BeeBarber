using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    public Transform cam;

    void LateUpdate()
    {
        if (cam) transform.LookAt(transform.position + cam.forward);
    }
}
