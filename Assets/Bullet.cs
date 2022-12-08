using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float timeToDisappear = 5f;

    private float timer = 0f;
    private bool useGravity = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useGravity)
        {
            if (timer >= timeToDisappear)
            {
                Destroy(gameObject);
                return;
            }
            timer += Time.deltaTime;
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "part" || collision.collider.tag == "hair") return; 
        if (collision.collider.tag == "Player")
        {
            BattleManage.Instance.playerTakeDamage(damage);
        }
        transform.GetComponent<Rigidbody>().useGravity = true;
        useGravity = true;
    }

    // Returns true if the bullet touch anything
   
}
