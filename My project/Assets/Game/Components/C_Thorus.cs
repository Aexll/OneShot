using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Thorus : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var tp = transform.position;
        if (tp.x > 30)
            tp.x -= 60;
        if (tp.y > 20)
            tp.y -= 40;
        if (tp.x < 30)
            tp.x += 60;
        if (tp.y < 20)
            tp.y += 40;
    }
}
