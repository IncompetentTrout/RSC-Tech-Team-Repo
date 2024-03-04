using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanic : MonoBehaviour
{
    #region Components
    [SerializeField] Rigidbody rb;
    #endregion

    #region Movement Variables
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Vector3 movement;
    [SerializeField] float moveSpeedMultiplier = 1f;
    #endregion

    #region Dash Variables
    [SerializeField] float dashTime = 0.3f;
    [SerializeField] float dashTimer;
    #endregion

    #region Booleans
    [SerializeField] bool canDash = true;
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

        rb.MovePosition(rb.position + movement * moveSpeed * moveSpeedMultiplier * Time.deltaTime);

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
                moveSpeedMultiplier = 5f;
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
                
                moveSpeedMultiplier = 1f;
            }
        }
    }
}
