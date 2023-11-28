using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Bonus : MonoBehaviour, IGameLoop
{

    public SO_GameInfo GI;

    public GameObject m_PrefabBonus;

    public GameObject[] prefabs;

    public GameObject visual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            var controller = collision.gameObject.GetComponent<C_BaseController>();
            if (controller != null)
            {
                controller.bulletPrefab_bonus = prefabs[Random.Range(0,prefabs.Length)];
                SetCollectedState(true);
            }
        }
    }

    public void SetCollectedState(bool collected)
    {
        visual.SetActive(!collected);
    }

    private void OnEnable()
    {
    }

    public void OnStartAim()
    {
        SetCollectedState(false);
    }

    public void OnStartGame()
    {
    }

    public void OnStartRewind()
    {
    }
}
