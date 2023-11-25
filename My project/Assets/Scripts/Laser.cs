using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer ;
    Transform mTransform;
    public float timer;

    public UnityEvent KillPlayer;
    private void Awake()
    {
        mTransform = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        StartCoroutine(LazerLoop());
    }
    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
        if (Physics2D.Raycast(mTransform.position, transform.right))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(mTransform.position, transform.right);
            Draw2DRay(laserFirePoint.position,hit.point);
        }
        else
        {
            
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }
    IEnumerator LazerLoop()
    {
        while (true)
        {
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(timer);
            lineRenderer.enabled = false;
            yield return new WaitForSeconds(timer);

        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
