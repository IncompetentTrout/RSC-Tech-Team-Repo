using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    //When setting up a checkpoint, add it to the array and add a Box Collider for the wall and a sphere collider
    //(or other non-box colliders)

    private Collider wall; //what blocks the player off after passing a checkpoint

    private void Start() {
        wall = gameObject.GetComponent<BoxCollider>(); //gets the box collider attached to the object
        wall.enabled = false; //disables the wall by default
    }

    private void OnTriggerEnter(Collider other) {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager>()
            .HitCheckPoint(gameObject); //calls the save-script to signify it has hit a checkpoint
        wall.enabled = true; //activates the wall behind the player
    }
}