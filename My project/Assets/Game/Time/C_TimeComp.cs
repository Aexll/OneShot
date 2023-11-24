using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class C_TimeComp : MonoBehaviour
{

    public GameTick GT;

    public Dictionary<int, TimeFrame> history;

    [Header("Optional")]
    public Rigidbody2D rb;
    public Transform rotating;

    // privates
    private Coroutine currentCoroutine;


    // infos
    [HideInInspector]
    public bool bInterpolating = false;

    private void Awake()
    {
        history = new Dictionary<int, TimeFrame>();
    }

    private void OnEnable()
    {
        GT.OnTickIndexChanged.AddListener(OnTickChanged);
    }

    private void OnDisable()
    {
        GT.OnTickIndexChanged.RemoveListener(OnTickChanged);
    }

    public void OnTickChanged()
    {
        OnFramePlay(GT.TickIndex);
    }

    // apply the frame at index if already in history, create it if not
    public void OnFramePlay(int frame)
    {
        if (history.ContainsKey(frame))
        {
            ApplyFrame(history[frame], !history.ContainsKey(frame+1));
        }
        else
        {
            history[frame] = GetCurrentTimeframe();
        }
    }

    // get current time frame
    public TimeFrame GetCurrentTimeframe()
    {
        TimeFrame tf = new TimeFrame();
        tf.position = transform.position;
        if (rb && rb.bodyType == RigidbodyType2D.Dynamic)
            tf.velocity = rb.velocity;
        if (rotating)
            tf.rotation = rotating.rotation.eulerAngles.z;
        return tf;
    }



    // apply frame
    public void ApplyFrame(TimeFrame tf, bool endInterp)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(interpolateTo(tf, GT.tickDelta, endInterp));
        //transform.position = tf.position;
        //if (rb)
        //    rb.velocity = tf.velocity;

    }

    public IEnumerator interpolateTo(TimeFrame tf, float time, bool endInterp)
    {
        bInterpolating = true;
        //lerpingpos = tf.position;
        if (rb)
        {
            //rb.simulated = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
        // init values
        Vector2 initialPos = transform.position;
        float initialRot = 0;
        if (rotating)
            initialRot = rotating.rotation.eulerAngles.z;

        rotating.rotation = Quaternion.Euler(new Vector3(0, 0, tf.rotation));

        // post ticks
        //if (rb) rb.velocity = Vector2.zero;

        // ticks
        float t = 0;
        while (t < time)
        {
            // get interp percent
            t += Time.deltaTime;
            float percent = t / time;
            percent = Mathf.Clamp01(percent);


            // position
            transform.position = Vector2.Lerp(initialPos, tf.position, percent);

            // rotation
           /* if (rotating)
            {
                float angle = Mathf.Lerp(initialRot, tf.rotation, percent);
                rotating.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }*/

            yield return null;
        }

        // post lerp

        if (endInterp)
        {
            bInterpolating = false;
            if (rb)
            {
                //rb.simulated = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = tf.velocity;
            }
        }
    }
}
