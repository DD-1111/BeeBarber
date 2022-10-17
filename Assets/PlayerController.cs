using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController cc;
    public float moveSpeed;
    public float jumpSpeed;

    private float horizontal, vertical;
    private Vector3 dir;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        vertical = Input.GetAxis("Vertical") * moveSpeed;
        dir = transform.forward * vertical + transform.right * horizontal;
        cc.Move(dir * Time.deltaTime);


    }
}
