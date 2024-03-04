using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMovement : Movement //inherited from Movement
{
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float 
        jumpForce,
        groundCheckDistance;

    [SerializeField]
    private LayerMask ground;

    private bool 
        isGrounded,
        hasJumped;




    // Update is called once per frame
    protected override void Update()
    {
        
            base.Update();
            
            if (Input.GetButtonDown("Jump") && isGrounded && !hasJumped) //if the player presses jump, is on the ground and is not in the middle of a jump
            {

                Jump();
                hasJumped = true;
            }

            CheckIfGrounded();
        


    }

    void CheckIfGrounded() //checks if player is on the ground
    {
        if(Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, ground)) //if a raycast down from the groundCheck position detects ground
        {
            isGrounded = true; 
            hasJumped = false;
        }
        else
        {
            isGrounded = false;
        }
       
    }


    void Jump()
    {
        Debug.Log("Jump");
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpForce, 0); //adds force upwards
    }

    private void OnDrawGizmos()
    {
       // Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
        Gizmos.DrawRay(groundCheck.position, Vector3.down * groundCheckDistance);
       // Gizmos.DrawLine(gameObject.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + layerChangeDist));
       // Gizmos.DrawLine(gameObject.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - layerChangeDist));
    }
}
