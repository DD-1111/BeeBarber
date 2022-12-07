using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float timeToDisappear = 5f;

    private HealthBar playerHealthBar;
    private float timer = 0f;
    private bool useGravity = false;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (useGravity)
        {
            if (timer >= timeToDisappear)
            {
                transform.gameObject.SetActive(false);
                return;
            }
            timer += Time.deltaTime;
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        if (collision.collider.tag == "Player")
        {
            playerHealthBar.TakeDamage(damage);
        }
        transform.GetComponent<Rigidbody>().useGravity = true;
        useGravity = true;
    }

    // Returns true if the bullet touch anything
   
}
