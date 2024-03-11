using UnityEngine;

public class TimeMovement : Movement {
	#region Variables

	[SerializeField] private Transform groundCheck;
	[SerializeField] private float jumpForce, groundCheckDistance;
	[SerializeField] private LayerMask ground;

	private bool isGrounded, hasJumped;

	#endregion

	protected override void Update() {
		base.Update();

		if (Input.GetButtonDown("Jump") && isGrounded && !hasJumped) {
			Jump();
			hasJumped = true;
		}

		CheckIfGrounded();
	}

	private void OnDrawGizmos() {
		Gizmos.DrawRay(groundCheck.position, Vector3.down * groundCheckDistance);
	}

	private void CheckIfGrounded() {
		if (Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, ground)) {
			isGrounded = true;
			hasJumped = false;
		}
		else {
			isGrounded = false;
		}
	}


	private void Jump() {
		Debug.Log("Jump");
		var velocity = rb.velocity;
		velocity = new Vector3(velocity.x, velocity.y + jumpForce, 0); //adds force upwards
		rb.velocity = velocity;
	}
}