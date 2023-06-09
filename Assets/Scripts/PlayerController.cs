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
    public GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    public float touchSpeed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

                spriteRenderer = GetComponent<SpriteRenderer>();

        // Calculate the bottom center position of the screen
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = spriteRenderer.bounds.extents.y;
        Vector3 startPosition = new Vector3(
            bounds.Center.x,
            bounds.BottomLeft.y + halfPlayerHeight,
            0
        );

        // Set the player's initial position to the bottom center of the screen
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        // Existing code for desktop platforms...
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        #elif UNITY_IOS || UNITY_ANDROID
        // Touch input for mobile platforms...
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                horizontalInput = touch.deltaPosition.x * touchSpeed;
                verticalInput = touch.deltaPosition.y * touchSpeed;
            }
        }
        #endif

        // The rest of your Update method...
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
        gameManager.ResetGame();
    }
}
