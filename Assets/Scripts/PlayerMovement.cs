using System;
using UnityEngine;
using UnityEngine.InputSystem;

//CURRENT CONTROLS: q d e a [fl, bl, fr, br]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private bool isMoving = false;
    // f = y
    [SerializeField] private float fAcel =  0, xAcel = 0;
    [SerializeField] private float xMaxVelo = 0, fMaxVelo = 0;
    [SerializeField] private const float MINVELO = 0;

    Vector2 moveInput, forceAppl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    //Called when a movement key is pressed
    public void OnMovement(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();   //Reads direction of x and y inputs
            isMoving = true;    //Sets the car to moving
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
            //Calcs final vector based on x & f accels and respective vector components
            forceAppl = new Vector2(xAcel * moveInput.normalized.x, fAcel * moveInput.normalized.y);

            rb.AddForce(forceAppl);     //Applies acceleration force to car

            // Clamps the max speed of the car with repect to direction
            rb.linearVelocity = 
                new Vector2(Mathf.Clamp(Mathf.Abs(rb.linearVelocity.x), MINVELO, xMaxVelo) * Mathf.Sign(moveInput.x), 
                Mathf.Clamp(Mathf.Abs(rb.linearVelocity.y), MINVELO, fMaxVelo) * Mathf.Sign(moveInput.y));

            //ADD ROTATION; SHOULD BE APPLIED TO COLLIDERS
        }
        if (!isMoving)
        {
            rb.linearVelocity = Vector2.zero; //TEMP ADD DECELERATION
        }
    }
}
