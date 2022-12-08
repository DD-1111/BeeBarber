using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHit : MonoBehaviour
{
    public GameObject screenFlash;
    public float delay = 1f;
    private float frames = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (screenFlash)
        {
            screenFlash.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        frames += Time.deltaTime;
        if (frames >= 2)
        {
            StartCoroutine(Flash());
            frames = 0;
        }
    }

    IEnumerator Flash()
    {
        Debug.Log("Flash");
        screenFlash.SetActive(true);
        yield return new WaitForSeconds(delay);
        screenFlash.SetActive(false);
    }
}
