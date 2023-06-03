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

        // Move the ship to the new position
        transform.position = newPosition;
    }
}
