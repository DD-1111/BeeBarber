using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour
{
    public float flashTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Flash()
    {
        transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        transform.gameObject.SetActive(false);
    }
}
