using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject door;

    
    private bool solved = false;

    
    public List<GameObject> switches = new List<GameObject>();
    public GameObject[] correctOrder;

      

    // Update is called once per frame
    void Update()
    {
        if (switches.Count == 0) return;

        solved = true;
        for(int i = 0; i < switches.Count; i++) {
            if (switches[i] != correctOrder[i])
            {
                solved = false;
                break;
            }
        }

        if (!solved)
        {

            for (int i = switches.Count - 1; i >= 0; i--)
            {
                switches[i].GetComponent<PuzzleButtonv2>().ResetButton();
            }
            return;
        }

        if (solved && switches.Count == correctOrder.Length)
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
