using UnityEngine;

public class GravityPanel : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null) {
            player.GravityDirection = -transform.up;
            player.SetRotation(Quaternion.LookRotation(other.transform.forward, transform.up));
        }
    }
}