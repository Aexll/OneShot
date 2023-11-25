using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SafeDestroy : MonoBehaviour, IGameLoop, IGameTick
{

    public GameObject[] todeactivate;

    private int tickinternal;
    private int destroyattick = -1;// -1 mean not yet

    public void OnStartAim()
    {
    }

    public void OnStartGame()
    {
        SafeAppear();
    }

    public void OnStartRewind()
    {
    }

    public void SafeDestroy()
    {
        foreach (var item in todeactivate)
        {
            item.SetActive(false);
        }
        transform.position = new Vector3(100, 0, 0);
        destroyattick = tickinternal;

    }

    public void SafeAppear()
    {
        foreach (var item in todeactivate)
        {
            item.SetActive(true);
        }
    }

    public void UpdateTick(int tick)
    {
        if(tick == destroyattick)
        {
            SafeDestroy();
        }
        tickinternal = tick;
    }
}
