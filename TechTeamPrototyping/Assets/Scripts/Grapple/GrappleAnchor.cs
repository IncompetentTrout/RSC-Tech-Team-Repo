using UnityEngine;

public class GrappleAnchor : MonoBehaviour {
	private GameObject enteredPlayer; //ref to the player that enters the area
	private GrappleHook hook; //ref to the GrappleHook script on the player

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
}