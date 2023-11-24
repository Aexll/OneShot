using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SpriteColorChanger : MonoBehaviour
{

    public SpriteRenderer sr;
    public Color color;

    public void setColor()
    {
        sr.color = color;
    }
}
