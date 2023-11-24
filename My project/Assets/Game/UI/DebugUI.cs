using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public SO_GameInfo GI;
    public GameTick GT;

    public TextMeshProUGUI txttick;
    public TextMeshProUGUI txtstate;

    private void OnEnable()
    {
        GI.StartRewind.AddListener(StartRewind);
        GI.StartAiming.AddListener(StartAim);
        GI.StartGame.AddListener(StartGame);
        GT.OnTickIndexChanged.AddListener(OnTickChanged);
    }

    private void OnDisable()
    {
        GI.StartRewind.RemoveListener(StartRewind);
        GI.StartAiming.RemoveListener(StartAim);
        GI.StartGame.RemoveListener(StartGame);
        GT.OnTickIndexChanged.RemoveListener(OnTickChanged);
    }

    public void OnTickChanged()
    {
        txttick.text = GT.TickIndex.ToString();
    }

    public void StartGame()
    {
        txtstate.text = GI.State.ToString();
    }

    public void StartAim()
    {
        txtstate.text = GI.State.ToString();
    }
    public void StartRewind()
    {
        txtstate.text = GI.State.ToString();
    }
}
