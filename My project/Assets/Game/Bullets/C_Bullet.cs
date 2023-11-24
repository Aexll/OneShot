using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class C_Bullet : MonoBehaviour
{

    public Transform orientTowardMovement;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // orient toward direction
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        orientTowardMovement.rotation = Quaternion.Euler(new Vector3(0, 0, angle));



    }

    
}
