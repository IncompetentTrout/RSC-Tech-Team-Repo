using UnityEngine;

public class CheckPoint : MonoBehaviour {

    #region Variables

    /*
     * When setting up a checkpoint,
     * add it to the array and add a Box Collider for the wall and a sphere collider
     * (or other non-box colliders)
     */
    private Collider wall;

    #endregion
    
    #region Unity Methods
    
    private void Awake() {
        wall = gameObject.GetComponent<BoxCollider>();
        wall.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager>().HitCheckPoint(gameObject); 
        wall.enabled = true; 
    }
    
    #endregion
}