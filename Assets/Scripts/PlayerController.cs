using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = spriteRenderer.bounds.extents.y;

        newPosition.x = Mathf.Clamp(newPosition.x, bounds.BottomLeft.x + halfPlayerWidth, bounds.TopRight.x - halfPlayerWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.BottomLeft.y + halfPlayerHeight, bounds.TopRight.y - halfPlayerHeight);

        // Move the ship to the new position
        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Restart current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
