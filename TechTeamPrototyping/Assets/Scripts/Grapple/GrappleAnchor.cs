using UnityEngine;

public class GrappleAnchor : MonoBehaviour {
	#region Variables

	private GameObject enteredPlayer;
	private GrappleHook hook;

	#endregion

	#region Unity Methods

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && enteredPlayer == null) {
			enteredPlayer = other.gameObject;
			hook = enteredPlayer.GetComponent<GrappleHook>();
			hook.EnterGrappleRadius(gameObject.transform.position);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player" && other.gameObject == enteredPlayer) {
			hook.ExitGrappleRadius(); //calls the appropriate function in the player's grappleHook script
			hook = null;
			enteredPlayer = null;
		}
	}

	#endregion
}