using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BaseController : MonoBehaviour
{

    public SO_InputMapping inputMapping;
    public Rigidbody2D rb;
    public float speed = 10;
    public Vector2 inputDirection;

    // Start is called before the first frame update
    void Start()
    {
        if(inputMapping == null)
        {
            Debug.LogAssertion("Controller needs a rigidbody reference");
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
        // update the direction
        Vector2 inputDirection = new Vector2(0, 0);
        if (Input.GetKey(inputMapping.KeyMoveUp)) inputDirection += new Vector2(0, 1);
        if (Input.GetKey(inputMapping.KeyMoveDown)) inputDirection += new Vector2(0, -1);
        if (Input.GetKey(inputMapping.KeyMoveLeft)) inputDirection += new Vector2(-1, 0);
        if (Input.GetKey(inputMapping.KeyMoveRight)) inputDirection += new Vector2(1, 0);
        inputDirection = inputDirection.normalized;

        rb.MovePosition(rb.position + (inputDirection * speed * Time.fixedDeltaTime));
        //Debug.Log(inputDirection);
    }
}
