using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float minXOffset = -5f;
    [SerializeField] private float maxXOffset = 5f;

    private Transform cameraTransform;
    private float targetXOffset;

    private void Start()
    {
        cameraTransform = transform;
        targetXOffset = Mathf.Clamp(ball.position.x, minXOffset, maxXOffset);
    }

    private void LateUpdate()
    {
        targetXOffset = Mathf.Clamp(ball.position.x, minXOffset, maxXOffset);

        Vector3 targetPosition = new Vector3(targetXOffset, cameraTransform.position.y, cameraTransform.position.z);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
