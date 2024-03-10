using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private GameObject
        target;

    [SerializeField] private float camSpeed;

    [SerializeField] private float
        xOffset,
        yOffset;

    private float
        xDifference,
        yDifference,
        baseZPos;

    private Camera cam;

    public float smoothTime = 0.3f;

    private void Start() {
        baseZPos = transform.position.z;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update() {
        Move();
    }

    private void Move() {
        var pos = transform.position;
        var tarPos = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset,
            target.transform.position.z);

        if (tarPos.x != pos.x || tarPos.y != pos.y) {
            xDifference = tarPos.x - pos.x;
            yDifference = tarPos.y - pos.y;
            //the cameras position and rate at which it moves changes depending on how large the x and y difference is, moving faster the larger the difference
            gameObject.transform.position = new Vector3(pos.x + xDifference * camSpeed * Time.deltaTime,
                pos.y + yDifference * camSpeed * Time.deltaTime, pos.z);
        }
    }

    public void UpdateTarget(GameObject newTarget) {
        target = newTarget;
    }
}