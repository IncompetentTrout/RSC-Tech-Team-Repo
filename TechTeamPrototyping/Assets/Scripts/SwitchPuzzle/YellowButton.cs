using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowButton : MonoBehaviour
{

    public GameObject player;
    public GameObject button;
    public bool buttonSwitched;
    public bool canPressButton;

    public Material colour;
    public Material activatedcolour;
    Renderer rend;

    private void OnTriggerEnter(Collider other)
    {
        canPressButton = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canPressButton = false;
    }

    private void PressButton()
    {
        if (canPressButton) 
        { 
            buttonSwitched = true;
            GetComponent<MeshRenderer>().material = activatedcolour;
            
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
        }
    }
}
