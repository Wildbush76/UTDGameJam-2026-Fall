using System;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

//CURRENT CONTROLS: q d e a [fl, bl, fr, br]
public class PlayerMovement : MonoBehaviour
{
    //New Version Please Work With ME;
    Rigidbody2D rb;

    [SerializeField] private bool isMoving = false, isSpin = false;
    // f = y
    [SerializeField] private float acel = 0;
    [SerializeField] private float xMaxVelo = 0, fMaxVelo = 0;
    [SerializeField] private const float MINVELO = 0;
    [SerializeField] float rotateAngle;
    [SerializeField] float rotateSpeed;

    Vector2 moveInput, forceAppl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        rotateAngle = transform.eulerAngles.z;
    }

    //Called when a movement key is pressed
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();   //Reads direction of x and y inputs
            isMoving = true;    //Sets the car to moving
            Debug.Log("forward value is " + moveInput.y);
            Debug.Log("steering value is " + moveInput.x);
        }
        if (context.canceled)
        {
            moveInput = Vector2.zero;   //Resets movement vector
            isMoving = false;   //Sets the car to still
        }
    }
    void FixedUpdate()
    {
        SpeedCalc();
    }

    void SpeedCalc()
    {
        if (isMoving)
        {
            TurnInDirection();
            //Calcs final vector based on x & f accels and respective vector components
            forceAppl = new Vector2(-moveInput.x * acel, moveInput.y * acel) * transform.up;
            //Debug.Log("sign of cos = " + Mathf.Sign(math.cos(rotateAngle * Mathf.Deg2Rad)));

            // * xAcel * moveInput.x  * fAcel * moveInput.y

            rb.AddForce(forceAppl);     //Applies acceleration force to car

            // Clamps the max speed of the car with repect to direction
            rb.linearVelocity =
                new Vector2(rb.linearVelocityX, Mathf.Clamp(Mathf.Abs(rb.linearVelocity.y), MINVELO, fMaxVelo) * moveInput.y);

            //ADD ROTATION; SHOULD BE APPLIED TO COLLIDERS
        }
        if (!isMoving)
        {
            rb.linearVelocity = Vector2.zero; //TEMP ADD DECELERATION
        }
    }

    public void TurnInDirection()
    {
        //rotateSpeed = math.cos(rotateAngle * Mathf.Deg2Rad) + 1.5f;
            //rotate the car clockwise or counter clockwise according to the direction its moveing in
        rotateAngle += rotateSpeed * -moveInput.x * Time.deltaTime;
        quaternion eular = quaternion.Euler(0, 0, rotateAngle);
        transform.rotation = eular;

            /*Quaternion test = transform.rotation;
            test.z += math.sign(moveInput.x) * rotateSpeed * Time.deltaTime * rotateSpeed;
            transform.rotation = test;*/
    }
    public void spin(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSpin = true;
        }
        else
        {
            isSpin = false;
        }
    }
}
