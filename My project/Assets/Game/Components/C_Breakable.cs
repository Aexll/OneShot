using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_Breakable : MonoBehaviour, IGameLoop, IGameTick, IDamageable
{

    public Sprite[] sprites;
    public SpriteRenderer sr;
    public LayerMask includeLayerMask;
    public Collider2D _collider;
    public GameObject visual;
    public bool rewindable;
    public int baseHealth = 1;
    private int health = 1;
    public List<int> breakTicks;
    public int currenttick = 0;

    public UnityEvent OnBreaked;

    public void OnStartAim()
    {
    }

    public void OnStartGame()
    {
        health = baseHealth;
    }

    public void OnStartRewind()
    {
        if (rewindable)
        {
            Restore();
        }
    }

    public void Restore()
    {
        health = baseHealth;
        visual.SetActive(true);
        _collider.enabled = true;
        sr.sprite = sprites[health];
    }

    public void Break()
    {
        visual.SetActive(false);
        _collider.enabled = false;
        OnBreaked?.Invoke();
    }

    private void Awake()
    {
        _collider.callbackLayers = includeLayerMask;
        _collider.contactCaptureLayers = includeLayerMask;
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(false);
    }*/

    public void DealDamage(bool replayed)
    {
        if(!replayed)
            breakTicks.Add(currenttick);
        health--;
        if (health <= 0)
        {
            Break();
        }
        else
        {
            if(health > 0 && health <= sprites.Length)
            sr.sprite = sprites[health-1];
        }
    }

    public void UpdateTick(int tick)
    {
        if (rewindable)
        {
            currenttick = tick;
            if (breakTicks.Contains(tick))
            {
                DealDamage(true);
            }
        }
    }

    public void TakeDamage(bool past, float damage)
    {
        if (!past)
        {
            DealDamage(false);
        }
    }
}
