using UnityEngine;

//Using old input system for now
public class DashMovement : MonoBehaviour {
    #region Components

    [SerializeField] private Rigidbody rb;

    #endregion

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody>();
        dashTimer = 0f;
    }

    // Update is called once per frame
    private void Update() {
        //Standard movemnet stuff
        movement.x = Input.GetAxisRaw("Horizontal");

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        grounded = Physics.CheckSphere(groundCheck.position, groundedDistance, groundMask);

        Dashing();

        //Jumping
        if (canJump && grounded) {
            if (Input.GetKeyUp(KeyCode.Space)) {
                canJump = !canJump;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else {
            canJump = !canJump;
        }
    }

    private void Dashing() {
        //Dash
        if (canDash) {
            //Reset Dash Timer
            dashTimer = 0f;

            //Reset Invincibility
            invincible = false;

            //Dash just increases movement speed, not a tp
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                canDash = false;
                if (movement.x > 0)
                    rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
                else
                    rb.AddForce(-Vector3.right * dashForce, ForceMode.Impulse);
            }
        }
        else {
            //Start dash and invincibility timer
            invincible = true;
            dashTimer += Time.deltaTime;

            //Check if dash and invincibility done
            if (dashTimer >= dashTime)
                if (grounded)
                    canDash = true;
        }
    }

    #region Movement Variables

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 movement;
    [SerializeField] private float moveSpeedMultiplier = 1f;

    #endregion

    #region Dash Variables

    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float dashTimer;
    [SerializeField] private float dashForce = 5f;

    #endregion

    #region Jump Variables

    //not final just added for testing
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundedDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    #endregion

    #region Booleans

    [SerializeField] private bool canDash = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool invincible;
    [SerializeField] private bool grounded = true;

    #endregion
}