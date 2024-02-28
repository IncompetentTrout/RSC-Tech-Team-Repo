using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        float moveValue = playerInputActions.Player.Movement.ReadValue<float>();
        print(moveValue);
    }
}
