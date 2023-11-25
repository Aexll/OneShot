using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanDirectionBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool bulletDirection = false;
    public float sidewaysforce = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bulletDirection = true;
    }

    private void FixedUpdate()
    {
        if (bulletDirection == true)
        {
            rb.AddForce(new Vector2 (sidewaysforce * Time.deltaTime, 0));
        }
    }
}
