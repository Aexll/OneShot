using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float speed;
    public float distanceBetween;

    private float distance;
    private float distance2;

    void Start()
    {
        
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player1.transform.position);
        Vector2 direction = player1.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        distance2 = Vector2.Distance(transform.position, player2.transform.position);
        Vector2 direction2 = player2.transform.position - transform.position;
        direction2.Normalize();
        float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;




        if (distance < distanceBetween && distance < distance2)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player1.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else if (distance2 < distanceBetween && distance2 < distance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player2.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle2);
        }
    }
}
