using UnityEngine;

public class CameraFollow : MonoBehaviour {
    #region Public Variables

    public Vector3 offset;
    public float smoothTime = 0.3f;

    #endregion

    #region Private Variables

    [SerializeField] private Transform target;
    private Vector3 _velocity = Vector3.zero;
    private bool _istargetNotNull;

    #endregion

    #region Methods

    private void Awake() {
        _istargetNotNull = target != null;
    }

    private void LateUpdate() {
        if (_istargetNotNull ) {
            
            var targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }
    
    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }

    #endregion
}