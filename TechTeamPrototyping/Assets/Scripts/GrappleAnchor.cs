using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAnchor : MonoBehaviour
{
    private GameObject enteredPlayer; //ref to the player that enters the area
    private GrappleHook hook; //ref to the GrappleHook script on the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enteredPlayer == null) //if the object that entered the area is a player and there isnt already a player inside the array
        {
            enteredPlayer = other.gameObject; //assigns the entered object to the player reference
            hook = enteredPlayer.GetComponent<GrappleHook>(); //gets the grapplehook script from the player
            hook.EnterGrappleRadius(gameObject.transform.position); //calls a function in the grapplehook script and passes through the game object 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && other.gameObject == enteredPlayer) //if the player that left is the assigned player
        {
            hook.ExitGrappleRadius(); //calls the appropriate function in the player's grappleHook script
            //clears the references so that nothing bad can happen (just in case)
            hook = null;
            enteredPlayer = null;
        }
    }

}
