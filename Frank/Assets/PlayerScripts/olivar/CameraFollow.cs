using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // The player
    public Vector3 offset;         // Camera offset relative to player
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Rotate the offset based on the player's current rotation
        Vector3 rotatedOffset = target.rotation * offset;

        // Calculate the desired position by adding the rotated offset to the player's position
        Vector3 desiredPosition = target.position + rotatedOffset;

        // Smooth the camera movement
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Remove or replace this line if it's affecting the up-down movement
        // This forces the camera to look at the player, but it can interfere with free vertical movement.
        // transform.LookAt(target);
    }
}
