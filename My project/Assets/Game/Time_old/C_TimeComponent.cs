using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct TimeFrame
{
    public Vector2 position;
    public Vector2 velocity;
    public float rotation;
    public float torque;
}


public class C_TimeComponent : MonoBehaviour
{

    public SO_TimeController TC;
    public Dictionary<int,TimeFrame> history;

    [Header("Optional")]
    public Rigidbody2D rb;
    public Transform rotating;


    public bool bInterpolating = false;
    public bool bTimePaused = false;


    // debug
    Vector2 lerpingpos;


    // privates
    private Coroutine currentCoroutine;

    private void Awake()
    {
        TC.PlayFrame.AddListener(OnFramePlay);
        history = new Dictionary<int,TimeFrame>();
    }

    private void OnDestroy()
    {
        TC.PlayFrame.RemoveListener(OnFramePlay);
    }


    // apply the frame at index if already in history, create it if not
    public void OnFramePlay(int frame)
    {
        if(history.ContainsKey(frame))
        {
           ApplyFrame(history[frame]);
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
        if(rb)
            tf.velocity = rb.velocity;
        if (rotating)
            tf.rotation = rotating.rotation.eulerAngles.z;
        return tf;
    }

    // apply frame
    public void ApplyFrame(TimeFrame tf)
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(interpolateTo(tf, TC.captureFrequency));
        //transform.position = tf.position;
        //if (rb)
        //    rb.velocity = tf.velocity;

    }

    public IEnumerator interpolateTo(TimeFrame tf, float time)
    {
        bInterpolating = true;
        lerpingpos = tf.position;

        // init values
        Vector2 initialPos = transform.position;
        float initialRot = 0;
        if (rotating)
            initialRot = rotating.rotation.eulerAngles.z;

        // post ticks
        if (rb) rb.velocity = Vector2.zero;

        // ticks
        float t = 0;
        while(t<time)
        {
            // get interp percent
            t += Time.deltaTime;
            float percent = t / time;
            percent = Mathf.Clamp01(percent);


            // position
            transform.position = Vector2.Lerp(initialPos, tf.position, percent);

            // rotation
            if (rotating)
            {
                float angle = Mathf.Lerp(initialRot, tf.rotation, percent);
                rotating.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            yield return null;
        }

        // post lerp
        if (rb) rb.velocity = tf.velocity;

        bInterpolating = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(lerpingpos, 0.1f);
    }
}
