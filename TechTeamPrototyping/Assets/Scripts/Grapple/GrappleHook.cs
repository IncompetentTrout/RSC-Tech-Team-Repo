using UnityEngine;

public class GrappleHook : MonoBehaviour {
	[SerializeField] private Transform grappleOrigin; //where the rope starts visually from the player

	//set of variables for setting the rope's properties
	[SerializeField] private float ropeSpringyness, ropeDampen, ropeMass, ropeMinScale, ropeMaxScale;

	private bool //booleans to ensure grappling cannot be done outside of grapple areas
		canGrapple,
		isGrappling;

	private Vector3 grappleAnchor;

	private SpringJoint joint;

	private LineRenderer lr;

	private void Start() {
		lr = gameObject.GetComponent<LineRenderer>();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.E)) //grapples to object
			GrappleBegin();
		if (Input.GetKeyUp(KeyCode.E)) //releases the grapple
			GrappleEnd();

		if (!canGrapple && !isGrappling) grappleAnchor = Vector3.zero;
	}

	//using late update to render the rope so that it does not lag behind the physics of the rope 
	private void LateUpdate() {
		DrawRope();
	}

	private void GrappleBegin() {
		if (grappleAnchor != Vector3.zero) {
			//Adds a springJoint component and connects it to the desired grappleAnchor
			joint = gameObject.AddComponent<SpringJoint>();
			joint.autoConfigureConnectedAnchor = false;
			joint.connectedAnchor = grappleAnchor;

			//gets the distance between the player and the grapple-point 
			var dist = Vector3.Distance(gameObject.transform.position, grappleAnchor);

			//distance the player can vary from grapple point
			joint.maxDistance = dist * ropeMaxScale;
			joint.minDistance = dist * ropeMinScale;

			//setting the Variables for rope's behaviour
			joint.spring = ropeSpringyness;
			joint.damper = ropeDampen;
			joint.massScale = ropeMass;

			//amount of points the line render can render too for the rope (2 points atm, can do some math for some visual bending of the rope but only 2 points minimum needed)
			lr.positionCount = 2;

			isGrappling = true;
		}
	}

	private void GrappleEnd() {
		//rope has no more need of existing visually, no more points needed
		lr.positionCount = 0;
		//rope has no more need of existing physically, destroy the joint
		Destroy(joint);
		isGrappling = false;
	}

	private void DrawRope() {
		if (!joint) return;

		lr.SetPosition(0, grappleOrigin.position);
		lr.SetPosition(1, grappleAnchor);
	}

	public void EnterGrappleRadius(Vector3 grapplePoint) {
		//if not currently grappling
		if (!isGrappling) {
			grappleAnchor = grapplePoint; //new grapple anchor is assigned
			canGrapple = true;
		}
	}

	public void ExitGrappleRadius() {
		canGrapple = false;
	}
}