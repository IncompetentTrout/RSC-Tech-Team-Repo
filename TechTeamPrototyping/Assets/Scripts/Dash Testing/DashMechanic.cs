using UnityEngine;

//Using old input system for now
public class DashMechanic : MonoBehaviour {
	#region Components

	[SerializeField] private Rigidbody rb;

	#endregion

	#region Movement Variables

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private Vector3 movement;
	[SerializeField] private float moveSpeedMultiplier = 1f;

	#endregion

	#region Dash Variables

	[SerializeField] private float dashTime = 0.3f;
	[SerializeField] private float dashForce = 10f;
	private float dashTimer;

	#endregion

	#region Booleans

	private bool canDash = true;
	private bool canJump = true;
	private bool invincible;
	private bool grounded = true;

	#endregion

	private void Start() {
		rb = GetComponent<Rigidbody>();
		dashTimer = 0f;
	}

	private void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");

		rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));

		Dashing();
	}

	private void Dashing() {
		if (!canDash) {
			HandleNonDashingState();
			return;
		}

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
}