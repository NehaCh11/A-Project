using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // Reference to the player's Transform
    public Vector3 offset;        // Offset between the camera and player
    public float smoothSpeed = 0.125f;  // Smoothing factor

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = player.position + offset;
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Make the camera always look at the player
            transform.LookAt(player);
        }
    }
}
