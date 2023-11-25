using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            IGameTick[] gameloopable = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IGameTick>().ToArray();
            foreach (var item in gameloopable)
            {
                item.UpdateTick(value);
            }
        }
    }

    // events
    public UnityEvent OnTickIndexChanged;
}
