using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.Two)) { 
            Debug.Log("11");
        }
    }
}

