using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CameraController camContr;
    public Rigidbody2D myRigidbody;
    public float velocityFactor;

    public float moveSpeed = 10.0f; // The speed at which the ship moves horizontally/vertically
    private float constantUpwardSpeed;
    private float horizontalInput;
    private float verticalInput;
    private CameraController.CameraBounds bounds;


    // Update is called once per frame
    void Update()
    {
        // Get the player's input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        constantUpwardSpeed = camContr.currentSpeed;
    
        // Calculate the new position of the ship
        Vector3 newPosition = transform.position + new Vector3(
            horizontalInput * moveSpeed * Time.deltaTime, 
            constantUpwardSpeed * Time.deltaTime + verticalInput * moveSpeed * Time.deltaTime,
             0
             );

        // Make sure the new position isn't outside the camera's bounds
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        newPosition.x = Mathf.Clamp(newPosition.x, bounds.BottomLeft.x, bounds.TopRight.x);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.BottomLeft.y, bounds.TopRight.y);

        // Move the ship to the new position
        transform.position = newPosition;
    }
}
