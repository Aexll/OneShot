using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTickManager : MonoBehaviour
{

    // know the game state
    public SO_GameInfo GI;

    public C_GameManager GM;


    public GameTick GT;


    public UnityEvent<int> OnTick;
    public UnityEvent<float> OnTickTime;
    public UnityEvent<string> OnTickTimeString;
    public UnityEvent<string> OnTickTimeLeftString;
    public UnityEvent<float> OnTickTimeLeftPercent;


    private void OnEnable()
    {
        GI.StartRewind.AddListener(StartRewind);
        GI.StartAiming.AddListener(StartAim);
        GI.StartGame.AddListener(StartGame);
    }

    private void OnDisable()
    {
        GI.StartRewind.RemoveListener(StartRewind);
        GI.StartAiming.RemoveListener(StartAim);
        GI.StartGame.RemoveListener(StartGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Tick), 0, GT.tickDelta);
    }

    public void Tick()
    { 
        if (!GT.isPaused)
            GT.TickIndex++;
        OnTick?.Invoke(GT.TickIndex);
        OnTickTime?.Invoke(GT.TickIndex * GT.tickDelta);
        OnTickTimeString?.Invoke((GT.TickIndex * GT.tickDelta).ToString());
        OnTickTimeLeftString?.Invoke((GM.LoopTime - (GT.TickIndex * GT.tickDelta)).ToString());
        OnTickTimeLeftPercent?.Invoke((GM.LoopTime - (GT.TickIndex * GT.tickDelta)));
    }

    public void StartGame()
    {
        GT.TickIndex = 0;
        GT.isPaused = false;
    }

    public void StartAim()
    {
        GT.isPaused = true;
    }
    public void StartRewind()
    {
        GT.isPaused = true;
        GT.TickIndex = 0;
    }


}
