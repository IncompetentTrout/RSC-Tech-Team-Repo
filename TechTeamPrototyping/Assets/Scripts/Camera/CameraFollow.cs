using UnityEngine;

public class CameraFollow : MonoBehaviour {
    #region Public Variables

    public Vector3 offset;
    public float smoothTime = 0.3f;

    #endregion

    #region Private Variables

    [SerializeField] private Transform target;
    private Vector3 velocity = Vector3.zero;

    #endregion

    #region Methods

    private void LateUpdate() {
        if (target != null) {
            var targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }

    #endregion
}