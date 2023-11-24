using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IM_", menuName = "ScriptableObjects/InputMapping", order = 1)]
public class SO_InputMapping : ScriptableObject
{

    public KeyCode KeyMoveUp;
    public KeyCode KeyMoveDown;
    public KeyCode KeyMoveLeft;
    public KeyCode KeyMoveRight;

    // ability
    public KeyCode KeyTriggerAbility;
    public UnityEvent AbilityTriggered;

}
