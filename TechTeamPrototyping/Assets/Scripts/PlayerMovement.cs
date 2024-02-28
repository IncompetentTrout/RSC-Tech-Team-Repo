using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravity;

    private PlayerInputActions playerInputActions;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleGravity();
    }

    private void HandleGravity() 
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
}
