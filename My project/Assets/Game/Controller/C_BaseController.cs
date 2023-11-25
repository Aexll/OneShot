using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_BaseController : MonoBehaviour
{
    // know the game state
    public SO_GameInfo GI;
    public C_GameManager GameManager;

    // infos
    public string playerName;


    public SO_InputMapping inputMapping;
    public Rigidbody2D rb;
    public float speed = 10;
    public float rotationSpeed = 10;
    public Vector2 inputDirection;
    

    public bool stopedMode = false;
    public bool moveMode = false;
    public bool aimMode = false;

    public GameObject bulletPrefab;


    public Vector2 defaultPosition;
    public float defaultRotationAngle;


    // events
    public UnityEvent OnPlayerDie;

    private void Awake()
    {
        defaultPosition = transform.position;
        defaultRotationAngle = transform.rotation.eulerAngles.z;
    }

    public void ResetPosAndRot()
    {
        transform.position = defaultPosition;
        transform.rotation = Quaternion.Euler(0, 0, defaultRotationAngle);
    }

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

    // Start is called before the first frame update
    void Start()
    {
        if(inputMapping == null)
        {
            Debug.LogAssertion("Controller needs a rigidbody reference");
        }
    }

    // start
    private void StartGame()
    {
        moveMode = true;
        aimMode = false;
        stopedMode = false;  

        GameObject spawnedbullet = Instantiate(bulletPrefab);
        spawnedbullet.transform.position = transform.position;
        spawnedbullet.transform.rotation = transform.rotation;
    }

    private void StartAim()
    {
        aimMode = true;
        moveMode = false;
        stopedMode = false;
    }

    private void StartRewind()
    {
        ResetPosAndRot();
        aimMode = false;
        moveMode = false;
        stopedMode = true;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "dmg")
        {
            OnPlayerDie?.Invoke();
            GI.OnPlayerWin?.Invoke(playerName);
            Destroy(gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {


        // trigger the ability
        if (Input.GetKeyUp(inputMapping.KeyTriggerAbility)) inputMapping.AbilityTriggered?.Invoke();
    }

    private void FixedUpdate()
    {


        if (moveMode)
        {
            // update the direction
            Vector2 inputDirection = new Vector2(0, 0);
            if (Input.GetKey(inputMapping.KeyMoveUp)) inputDirection += new Vector2(0, 1);
            if (Input.GetKey(inputMapping.KeyMoveDown)) inputDirection += new Vector2(0, -1);
            if (Input.GetKey(inputMapping.KeyMoveLeft)) inputDirection += new Vector2(-1, 0);
            if (Input.GetKey(inputMapping.KeyMoveRight)) inputDirection += new Vector2(1, 0);
            inputDirection = inputDirection.normalized;

            rb.MovePosition(rb.position + (inputDirection * speed * Time.fixedDeltaTime));
            // transform.rotation = Quaternion.Euler(0, 0, 0);

            // rotation
            if(inputDirection.sqrMagnitude>0.1f)
            {
                float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

        }

        if(aimMode)
        {
            var z = transform.rotation.eulerAngles.z;
            if (Input.GetKey(inputMapping.KeyMoveLeft)) z += Time.fixedDeltaTime * rotationSpeed;
            if (Input.GetKey(inputMapping.KeyMoveRight)) z -= Time.fixedDeltaTime * rotationSpeed;
            transform.rotation = Quaternion.Euler(0,0,z);
        }
        //Debug.Log(inputDirection);
    }
}
