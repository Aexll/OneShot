using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce;
    public float moveSpeed;

    public float smoothSpeed;
    Vector2 reference;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Mathf.Abs(rb.velocity.y) <= 0.0000001f)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        Vector2 newVelocity = rb.velocity;


        if (Input.GetKey(KeyCode.D))
        {
            newVelocity.x = moveSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newVelocity.x = -moveSpeed;
        }

        rb.velocity = Vector2.SmoothDamp(rb.velocity, newVelocity, ref reference, smoothSpeed);
    }
}
