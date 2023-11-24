using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTickManager : MonoBehaviour
{

    // know the game state
    public SO_GameInfo GI;


    public GameTick GT;


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
    }


}
