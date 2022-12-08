using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHit : MonoBehaviour
{
    public ScreenFlash screenFlash;
    private float frames = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        frames += Time.deltaTime;
        if (frames >= 2 && screenFlash)
        {
            StartCoroutine(screenFlash.Flash());
            frames = 0;
        }
    }
}
