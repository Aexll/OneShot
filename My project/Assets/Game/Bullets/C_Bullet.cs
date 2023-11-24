using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.UI.ContentSizeFitter;

public class C_Bullet : MonoBehaviour
{
    public GameTick GT;
    public SO_GameInfo GI;
    public C_TimeComp timecomp;

    public Transform orientTowardMovement;
    public Rigidbody2D rb;
    public Collider2D _collider;

    public float impulse = 1;


    private void OnEnable()
    {
        GI.StartRewind.AddListener(StartRewind);
        GI.StartAiming.AddListener(StartAim);
        GI.StartGame.AddListener(StartGame);
    }

    private void OnDisable()
    {
        GI.StartRewind.RemoveListener(StartRewind);
        GI.StartAiming.RemoveListener(StartAim);
        GI.StartGame.RemoveListener(StartGame);
    }



    private void Start()
    {
        StartGame();
    }

    // start
    private void StartGame()
    {
        orientTowardMovement.gameObject.SetActive(true);
        //_collider.enabled = false;
        LayerMask masks = new LayerMask();
        masks.value = LayerMask.GetMask("Bullet", "Player");
        _collider.excludeLayers = masks;
        Invoke(nameof(EnableCollision), 0.5f);
        if(rb.bodyType == RigidbodyType2D.Dynamic)
            rb.AddForce(transform.right * impulse, ForceMode2D.Impulse);
    }

    private void StartAim()
    {
        _collider.enabled = false;
    }

    private void StartRewind()
    {
        orientTowardMovement.gameObject.SetActive(false);
        _collider.enabled = false;
    }

    public void EnableCollision()
    {
        //_collider.enabled = true;
        LayerMask masks = new LayerMask();
        masks.value = LayerMask.GetMask("Bullet");
        _collider.excludeLayers = masks;
    }

    // Update is called once per frame
    void Update()
    {
        if (GT.isPaused && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = Vector2.zero;
        }

        if (!timecomp.bInterpolating && !GT.isPaused)
        {
            // orient toward direction
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            orientTowardMovement.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    
}
