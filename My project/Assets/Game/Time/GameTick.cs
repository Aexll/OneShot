using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameTick", menuName = "ScriptableObjects/GameTick", order = 1)]
public class GameTick : ScriptableObject
{

    public float tickDelta = 0.1f;
    public bool isPaused = false;

    int tickIndex;
    public int TickIndex
    {
        get { return tickIndex; }
        set {
            tickIndex = value;
            OnTickIndexChanged?.Invoke();
        }
    }

    // events
    public UnityEvent OnTickIndexChanged;
}
