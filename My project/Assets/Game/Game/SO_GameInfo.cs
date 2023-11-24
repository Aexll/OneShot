using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum GameState
{
    Aiming,
    Game,
    Rewind,
}

[CreateAssetMenu(fileName = "GI_", menuName = "ScriptableObjects/GameInfo")]
public class SO_GameInfo : ScriptableObject
{
    // events
    public UnityEvent StartAiming;
    public UnityEvent StartGame;
    public UnityEvent StartRewind;
    public UnityEvent<string> OnPlayerWin;

    // values
    public GameState State;
}
