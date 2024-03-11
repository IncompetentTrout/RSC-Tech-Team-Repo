using UnityEngine;

public class DashMovement : MonoBehaviour {
	
	#region Configurable Variables
	
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float dashTime = 0.3f;
	[SerializeField] private float dashForce = 5f;
	[SerializeField] private float jumpForce = 1000f;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundedDistance = 0.4f;
	[SerializeField] private LayerMask groundMask;
	
	#endregion
	
	#region Variables
	
	private float dashTimer;
	private Vector3 movement;
	private float moveSpeedMultiplier = 1f;
	private bool canDash = true;
	private bool canJump = true;
	private bool invincible;
	private bool grounded = true;
	
	#endregion
	
	#region Unity Methods

	private void Start() {
		rb = GetComponent<Rigidbody>();
		dashTimer = 0f;
	}

	private void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");

		rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));

		grounded = Physics.CheckSphere(groundCheck.position, groundedDistance, groundMask);

		Dashing();

		// dealing with jumping
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

	#endregion

	#region Methods

	private void Dashing() {
		if (!canDash) {
			HandleNonDashingState();
			return;
		}

		// Reset dash state assuming canDash is true.
		dashTimer = 0f;
		invincible = false;

		if (!Input.GetKeyDown(KeyCode.LeftShift)) return;

		canDash = false;
		var dashDirection = movement.x > 0 ? Vector3.right : -Vector3.right;
		rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
	}

	private void HandleNonDashingState() {
		invincible = true;
		dashTimer += Time.deltaTime;

		if (dashTimer >= dashTime && grounded) canDash = true;
	}

	#endregion
}