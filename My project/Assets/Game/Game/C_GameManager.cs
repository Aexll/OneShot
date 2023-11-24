using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_GameManager : MonoBehaviour
{

    public SO_GameInfo GI;

    public bool isInLoop = false;

    [Header("delays")]
    public float AimTime = 3;
    public float RewindTime = 1;
    public float LoopTime = 5;
    public float LoopIncrement = 2;
    public float WinScreenTime = 2;


    public UnityEvent OnStartAim;
    public UnityEvent OnStartGame;
    public UnityEvent OnStartRewind;
    public UnityEvent<string> OnPlayerWin; // triggered in controller
    public UnityEvent OnChangeScene;


    public void PlayerWin(string pname)
    {
        StartRewind();
        StopCoroutine(gameloopcr);
        Invoke(nameof(DoChangeScene), 2);
    }

    public void DoChangeScene()
    {
        OnChangeScene?.Invoke();
    }

    private void OnEnable()
    {
        GI.OnPlayerWin.AddListener(PlayerWin);
    }

    private void OnDisable()
    {
        GI.OnPlayerWin.RemoveListener(PlayerWin);
    }

    Coroutine gameloopcr;

    private void Start()
    {
        //InvokeRepeating(nameof(LaunchAGameLoop), 0, 10);
        gameloopcr = StartCoroutine(GameLoop());
    }


    private IEnumerator GameLoop()
    {
        while (true)
        {
            StartAim();
            yield return new WaitForSeconds(AimTime);
            StartGame();
            yield return new WaitForSeconds(LoopTime);
            StartRewind();
            yield return new WaitForSeconds(RewindTime);
        }
    }

    public void StartAim()
    {
        GI.State = GameState.Aiming;
        GI.StartAiming?.Invoke();
        OnStartAim?.Invoke();
    }

    public void StartGame()
    {
        GI.State = GameState.Game;
        GI.StartGame?.Invoke();
        OnStartGame?.Invoke();
    }

    public void StartRewind()
    {
        LoopTime += LoopIncrement;
        GI.State = GameState.Rewind;
        GI.StartRewind?.Invoke();
        OnStartRewind?.Invoke();
    }
}
