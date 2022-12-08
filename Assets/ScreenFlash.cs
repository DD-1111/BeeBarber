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

    public IEnumerator Fade()
    {
        CanvasGroup cg = transform.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        transform.gameObject.SetActive(true);
        for (int i = 1; i <= 10; i++)
        {
            yield return new WaitForSeconds(1.5f * Time.deltaTime);
            cg.alpha = 0.1f * i;

        }
        yield return new WaitForSeconds(10f * Time.deltaTime);
        for (int i = 10; i >= 1; i--)
        {
            yield return new WaitForSeconds(1.5f * Time.deltaTime);
            cg.alpha = 0.1f * i;

        }
        transform.gameObject.SetActive(false);
    }
}
