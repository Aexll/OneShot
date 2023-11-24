using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_GameManager : MonoBehaviour
{

    public SO_GameInfo GI;

    public bool isInLoop = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InvokeRepeating(nameof(LaunchAGameLoop), 0, 10);
    }

    public void LaunchAGameLoop()
    {
        isInLoop = true;
        StartAim();
        Invoke(nameof(StartGame), 2);
        Invoke(nameof(StartRewind), 9);
        isInLoop = false;
    }

    public void StartAim()
    {
        GI.State = GameState.Aiming;
        GI.StartAiming?.Invoke();
    }

    public void StartGame()
    {
        GI.State = GameState.Game;
        GI.StartGame?.Invoke();
    }

    public void StartRewind()
    {
        GI.State = GameState.Rewind;
        GI.StartRewind?.Invoke();
    }
}
