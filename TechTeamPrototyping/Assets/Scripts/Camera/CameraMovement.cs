using UnityEngine;

public class CameraMovement : MonoBehaviour {

    #region  Private Fields
    
    private Camera cam;
    
    [SerializeField] private GameObject target;

    [SerializeField] private float camSpeed;
    [SerializeField] private float xOffset, yOffset;
    
    #endregion

    #region  Public Fields
    
    public float smoothTime = 0.3f;
    private float _xDifference, _yDifference, _baseZPos;
    
    #endregion

    #region Unity Methods
    private void Start() {
        _baseZPos = transform.position.z;
        cam = GetComponent<Camera>();
    }
    
    private void Update() {
        Move();
    }
    
    #endregion

    #region Methods
    
    private void Move() {
        var pos = transform.position;
        var position = target.transform.position;
        
        var tarPos = new Vector3(position.x + xOffset, position.y + yOffset, position.z);

        if (tarPos.x != pos.x || tarPos.y != pos.y) {
            _xDifference = tarPos.x - pos.x;
            _yDifference = tarPos.y - pos.y;
            //the cameras position and rate at which it moves changes depending on how large the x and y difference is, moving faster the larger the difference
            gameObject.transform.position = new Vector3(pos.x + _xDifference * camSpeed * Time.deltaTime,
                pos.y + _yDifference * camSpeed * Time.deltaTime, pos.z);
        }
    }

    public void UpdateTarget(GameObject newTarget) {
        target = newTarget;
    }
    
    #endregion
}