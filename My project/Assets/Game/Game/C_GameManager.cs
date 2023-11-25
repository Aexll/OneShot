using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;



interface IGameLoop
{
    public void OnStartAim();
    public void OnStartGame();
    public void OnStartRewind();
}

interface IGameTick
{
    public void UpdateTick(int tick);
}

public class C_GameManager : MonoBehaviour
{

    public SO_GameInfo GI;

    public bool isInLoop = false;

    [Header("delays")]
    public float AimTime = 3;
    public float RewindTime = 1;
    public float LoopTime = 5;
    public float LoopIncrement = 2;
    public float PreRewindOffset = 0.5f;
    public float WinScreenTime = 2;


    public UnityEvent OnStartAim;
    public UnityEvent OnStartGame;
    public UnityEvent OnStartPrerewind;
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
            yield return new WaitForSeconds(LoopTime - PreRewindOffset);
            OnStartPrerewind?.Invoke();
            yield return new WaitForSeconds(PreRewindOffset);
            StartRewind();
            yield return new WaitForSeconds(RewindTime);
        }
    }

    public void StartAim()
    {
        GI.State = GameState.Aiming;
        GI.StartAiming?.Invoke();
        OnStartAim?.Invoke();
        //GameObjectExtensions.GetInterfaces<IGameLoop>(GameObject);

        IGameLoop[] gameloopable = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IGameLoop>().ToArray();
        foreach (var item in gameloopable)
        {
            item.OnStartAim();
        }
    }

    public void StartGame()
    {
        GI.State = GameState.Game;
        GI.StartGame?.Invoke();
        OnStartGame?.Invoke();

        IGameLoop[] gameloopable = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IGameLoop>().ToArray();
        foreach (var item in gameloopable)
        {
            item.OnStartGame();
        }
    }

    public void StartRewind()
    {
        LoopTime += LoopIncrement;
        GI.State = GameState.Rewind;
        GI.StartRewind?.Invoke();
        OnStartRewind?.Invoke();

        IGameLoop[] gameloopable = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IGameLoop>().ToArray();
        foreach (var item in gameloopable)
        {
            item.OnStartRewind();
        }
    }
}
