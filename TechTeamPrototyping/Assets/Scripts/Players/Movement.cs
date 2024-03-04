using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float 
        _speed,
        layerChangeDist;

   // [SerializeField]
  //  private GameObject prefab; //used in "Shoot()"

    [SerializeField]
    private int layerCount; //amount of layers in the scene starting from 0, each layer is further away from camera than layer 0

    private int currentLayer;

    protected bool 
        canShiftLayers,
        isActive;

    private float horizontalInput;

    protected Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       
        // _speed = 25f;
        // transform.position = (Vector3.zero);
        rb = GetComponent<Rigidbody>();
        currentLayer = 1; //player by default will be on layer 1 (can move towards camera by default)
    }

    // Update is called once per frame
    protected virtual void Update()
    {
      
            Move();

            CheckInput();
        

    }

    void CheckInput()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    Shoot();
        //}

        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeLayer(1); //move layer away from camera (positive on z axis)
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeLayer(-1); //move layer towards camera (negative on z axis)

        }

    }

    protected void Move()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x + (horizontalInput * _speed * Time.deltaTime), pos.y, pos.z);

        //transform.Translate(Vector3.forward * horizontalInput * _speed * Time.deltaTime); //move along x-axis by input and speed
        

    }


    protected void ChangeLayer(int direction) //moves the player back or forth on the layer
    {

        Vector3 checkPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z); //current player position with check closer to feet of player
        switch (direction)//checking if there is an object blocking where the desired layer move is
        {
            case -1:
                canShiftLayers = !Physics.Raycast(checkPos, Vector3.back, layerChangeDist); //if the raycast towards camera returns a hit, the player cannot shift
                break;
            case 1:
                canShiftLayers = !Physics.Raycast(checkPos, Vector3.forward, layerChangeDist);//if the raycast away from camera returns a hit, the player cannot shift
                break;
        }


        if (direction + currentLayer > layerCount || direction + currentLayer < 0) //if the input is outside of the amount of layers they can move
        {
            return; //return so the player wont move
        }
        else if (canShiftLayers) //otherwise if they can shift
        {
            currentLayer += direction; //current layer is now the appropriate layer
            Vector3 pos = gameObject.transform.position;
            gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z + (direction * layerChangeDist)); //move the player by the required distance in the right direction to be on the layer
        }
    }

    public void ResetLayer()
    {
        currentLayer = 1;
    }


    public void Toggle()
    {
       isActive = !isActive;

        switch (isActive) 
        {
            case true:
                gameObject.tag = "Player";
                break;
            case false:
                gameObject.tag = "DeactivePlayer";
                break;
        }
    }




    //public void ForceMove()
    //{
    //    horizontalInput = Input.GetAxis("Horizontal");
    //    //rb.AddForce(Vector3.right , ForceMode.Force);
    //    rb.velocity = new Vector3(rb.velocity.x * horizontalInput * _speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
    //    // verticalInput = Input.GetAxis("Vertical");
    //    // rb.AddForce(Vector3.forward * verticalInput * _speed * Time.deltaTime, ForceMode.Force);
    //}

    //public void Shoot()
    //{
    //    Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    //}

    //private void OnDrawGizmos()
    //{

    //    Gizmos.DrawLine(gameObject.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + layerChangeDist));
    //    Gizmos.DrawLine(gameObject.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - layerChangeDist));
    //}
}