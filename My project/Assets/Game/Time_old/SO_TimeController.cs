using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TC_", menuName = "ScriptableObjects/TimeController", order = 1)]
public class SO_TimeController : ScriptableObject
{

    public UnityEvent<int> PlayFrame;
    public float captureFrequency; // time bewtween frames
    public bool timePaused = false; // tick paused
    public float timeSpeed = 1.0f; // time between tick
    public int tickIndex;
}
