using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float speed = 5;
    Rigidbody rb;
    float hMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 forward = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 side = transform.right *hMove * 0.1f; //make it discrete 
        rb.MovePosition(rb.position + forward +side);
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxis("Horizontal");
    }
}
