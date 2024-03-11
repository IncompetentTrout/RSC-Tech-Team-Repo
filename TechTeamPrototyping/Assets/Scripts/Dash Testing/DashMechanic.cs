using UnityEngine;

public class DashMechanic : MonoBehaviour {
	
	#region Variables
	
	[SerializeField] private Rigidbody rb;
	
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float dashTime = 0.3f;
	[SerializeField] private float dashForce = 10f;
	
	private Vector3 movement;
	private float dashTimer;
	private bool canDash = true;
	private bool invincible;
	private bool grounded = true;
	
	#endregion
	
	#region Unity Methods

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");
		if (Input.GetKeyDown(KeyCode.LeftShift)) Dashing();
		HandleNonDashingState();
	}

	private void FixedUpdate() {
		rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
	}
	
	#endregion
	
	#region Methods

	private void Dashing() {
		if (!canDash) return;

		dashTimer = 0f;
		invincible = false;

		if (!Input.GetKeyDown(KeyCode.LeftShift)) return;

		canDash = false;
		var dashDirection = movement.x > 0 ? Vector3.right : Vector3.left;
		rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
	}

	private void HandleNonDashingState() {
		if (canDash) return;

		invincible = true;
		dashTimer += Time.deltaTime;

		if (dashTimer >= dashTime && grounded) {
			canDash = true;
			invincible = false;
		}
	}
	
	#endregion
}