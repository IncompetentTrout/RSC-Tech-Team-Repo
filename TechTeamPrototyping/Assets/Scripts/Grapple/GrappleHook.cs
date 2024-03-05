using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [SerializeField]
    private Transform grappleOrigin; //where the rope starts visually from the player

    //set of variables for setting the rope's properties
    [SerializeField]
    private float
        ropeSpringyness,
        ropeDampen,
        ropeMass,
        ropeMinScale,
        ropeMaxScale;

    private SpringJoint joint; //the physics rope

    private LineRenderer lr; //the visual rope

    private Vector3 grappleAnchor; //where the rope anchors visually and physically, taken in from an outside source

    private bool //booleans to ensure grappling cannot be done outside of grapple areas
        canGrapple,
        isGrappling;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>(); //gets the line-Renderer component on the player
    }

    // Update is called once per frame
    void Update()
    {

        //switch for "GetButton" when you get the chance
        if (Input.GetKeyDown(KeyCode.E)) //grapples to object
        {
            GrappleBegin();
        }
        if(Input.GetKeyUp(KeyCode.E)) //releases the grapple
        {
            GrappleEnd();
        }

        if (!canGrapple && !isGrappling) //if not currently grappling and not outside the active area (in case the rope stretches outside the trigger area)
        {
            grappleAnchor = Vector3.zero; //0'ing out the grapple as a "reset" because null cannot be assigned
        }

    }

    //using late update to render the rope so that it does not lag behind the physics of the rope 
    private void LateUpdate()
    {
        DrawRope();
    }


    private void GrappleBegin()
    {
        if(grappleAnchor != Vector3.zero) //if the grapple point is not "Reset"
        {
            //Adds a springJoint component and connects it to the desired grappleAnchor
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grappleAnchor;

            //gets the distance between the player and the grapple-point 
            float dist = Vector3.Distance(gameObject.transform.position, grappleAnchor);

            //distance the player can vary from grapple point
            joint.maxDistance = dist * ropeMaxScale; //smaller scale, the less it can stretch
            joint.minDistance = dist * ropeMinScale; //smaller scale, the less it can 

            //setting the Variables for rope's behaviour
            joint.spring = ropeSpringyness;
            joint.damper = ropeDampen;
            joint.massScale = ropeMass;

            //amount of points the line render can render too for the rope (2 points atm, can do some math for some visual bending of the rope but only 2 points minimum needed)
            lr.positionCount = 2;

            isGrappling = true;
        }
    }




    private void GrappleEnd()
    {
        //rope has no more need of existing visually, no more points needed
        lr.positionCount = 0;
        //rope has no more need of existing physically, destroy the joint
        Destroy(joint);
        isGrappling = false;

        
    }

    private void DrawRope()
    {
        if (!joint) //if draw-rope is called while there is no joint (when joint is destroyed)
        {
            return;
        }
        //rope points for it to draw between
        lr.SetPosition(0, grappleOrigin.position);
        lr.SetPosition(1, grappleAnchor);
    }

    //called by grapple point when they enter the trigger radius
    public void EnterGrappleRadius(Vector3 grapplePoint)
    {
        //if not currently grappling
        if (!isGrappling)
        {
            grappleAnchor = grapplePoint; //new grapple anchor is assigned
            canGrapple = true;
        }

    }

    public void ExitGrappleRadius()
    {
        canGrapple = false;
    }
}
