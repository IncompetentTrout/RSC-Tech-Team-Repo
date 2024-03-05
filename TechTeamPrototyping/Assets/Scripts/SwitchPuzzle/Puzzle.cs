using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject door;

    YellowButton yellow;
    RedButton red;
    GreenButton green;
    BlueButton blue;

    private bool solved;

    private bool switch1;
    private bool switch2;
    private bool switch3;
    private bool switch4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (red.buttonSwitched = true) 
        {
            switch1 = true;
            Debug.Log("switch 1 on");
            if (blue.buttonSwitched = true)
            {
                switch2 = true;
                if(yellow.buttonSwitched = true)
                {
                    switch3 = true;
                    if(green.buttonSwitched = true)
                    {
                        switch4 = true;
                    }
                }
            }
        }

        if (switch4 = true)
        {
            OpenDoor();
            solved = true;
        }
    }

    private void OpenDoor()
    {
        Destroy(door);
    }
}
