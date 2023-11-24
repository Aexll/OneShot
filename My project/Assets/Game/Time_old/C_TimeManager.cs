using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TimeManager : MonoBehaviour
{

    public SO_GameInfo GI;
    public SO_TimeController TC;

    public bool paused
    {
        get { return TC.timePaused; }
        set { TC.timePaused = value; }
    }

    public int tickIndex
    {
        get { return TC.tickIndex; }
        set { TC.tickIndex = value; }
    }


    private void Start()
    {
        GI.StartAiming.AddListener(OnStartAiming);
        GI.StartGame.AddListener(OnStartGame);
        GI.StartRewind.AddListener(OnStartRewind);
        StartCoroutine(TickTimer());
    }

    private void OnDestroy()
    {
        GI.StartAiming.RemoveListener(OnStartAiming);
        GI.StartGame.RemoveListener(OnStartGame);
        GI.StartRewind.RemoveListener(OnStartRewind);
    }

    IEnumerator TickTimer()
    {
        while (true)
        {
            if (!paused)
            {
                yield return new WaitForSeconds(TC.captureFrequency);
                Tick(TC.timeSpeed>=0);
            } else
            {
                yield return null;
            }

        }
    }

    public void Tick(bool timeDirection)
    {
        if (!paused)
        {
            tickIndex += timeDirection ? -1 : 1;

            if (tickIndex <= 0) tickIndex = 0;

            TC.PlayFrame?.Invoke(tickIndex);
        }
    }


    void OnStartAiming()
    {
        paused = true;
    }

    void OnStartGame()
    {
        paused = false;
        TC.timeSpeed = 1;
    }

    void OnStartRewind()
    {
        paused = false;
        TC.timeSpeed = -1;
    }

}
