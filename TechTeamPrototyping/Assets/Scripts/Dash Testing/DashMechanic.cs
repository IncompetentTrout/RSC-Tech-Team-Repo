using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using old input system for now
public class DashMechanic : MonoBehaviour
{
    #region Components
    [SerializeField] Rigidbody rb;
    #endregion

    #region Movement Variables
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 movement;
    [SerializeField] float moveSpeedMultiplier = 1f;
    #endregion

    #region Dash Variables
    [SerializeField] float dashTime = 0.3f;
    [SerializeField] float dashTimer;
    [SerializeField] float dashForce = 10f;
    #endregion

    #region Booleans
    [SerializeField] bool canDash = true;
    [SerializeField] bool canJump = true;
    [SerializeField] bool invincible = false;
    [SerializeField] bool grounded = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dashTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Standard movemnet stuff
        movement.x = Input.GetAxisRaw("Horizontal");

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        Dashing();

    }

    void Dashing()
    {
        //Dash
        if (canDash)
        {
            //Reset Dash Timer
            dashTimer = 0f;

            //Reset Invincibility
            invincible = false;

            //Dash just increases movement speed, not a tp
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                canDash = false;
                if(movement.x > 0)
                {
                    rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(-Vector3.right * dashForce, ForceMode.Impulse);
                }
            }

        }
        else
        {
            //Start dash and invincibility timer
            invincible = true;
            dashTimer += Time.deltaTime;

            //Check if dash and invincibility done
            if (dashTimer >= dashTime)
            {
                if (grounded)
                {
                    canDash = true;
                }
                
                
            }
        }
    }
}
