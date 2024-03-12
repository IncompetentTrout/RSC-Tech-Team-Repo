using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    Puzzle pS;


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
            pS.switches.Add(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
        }
    }

    public void ResetButton()
    {
        GetComponent<MeshRenderer>().material = colour;
        buttonSwitched = false;
        pS.switches.Remove(gameObject);
    }
}
