using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_Explode : MonoBehaviour, IGameTick, IGameLoop
{

    public float explosionDuration = 0.4f;
    public float explosionRadius = 1;

    public CircleCollider2D cc;

    public UnityEvent OnTriggered;
    public UnityEvent OnEndTriggered;

    private float internalTick = 0;
    private float triggerAtTick = -1;

    private void Awake()
    {
        cc.radius = 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Explode();
    }

    public void Explode()
    {
        OnTriggered?.Invoke();
        Invoke(nameof(ExplosionEnd), explosionDuration);
        StartCoroutine(explodesize());
    }

    public void ExplosionEnd()
    {
        OnEndTriggered?.Invoke();
    }

    IEnumerator explodesize()
    {
        float t = 0;
        while(t<explosionDuration)
        {
            t += Time.deltaTime;
            cc.radius = (t / explosionDuration)* explosionRadius;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var i = collision.GetComponent<IDamageable>();
        if(i != null)
        {
            i.TakeDamage(triggerAtTick == -1,1);
        }
    }

    public void UpdateTick(int tick)
    {
        internalTick = tick;
        if(tick == triggerAtTick)
        {
            Explode();
        }
    }

    public void OnStartAim()
    {
    }

    public void OnStartGame()
    {
    }

    public void OnStartRewind()
    {
    }
}
