using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxDust : MonoBehaviour
{
    public float speed = 0.5f; // speed of the parallax effect, adjust to your needs
    private ParticleSystem dustParticles;
    public CameraController cameraController; // reference to the CameraController

    void Start()
    {
        dustParticles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // get the current camera bounds
        var bounds = CameraController.GetCameraBounds(cameraController.mainCam);

        // reset the position of the particle system once it's off screen
        if (transform.position.y <= bounds.BottomLeft.y)
        {
            // reset position to top of camera view
            transform.position = new Vector3(transform.position.x, bounds.TopLeft.y, transform.position.z);
            
            // clear the current particles so it looks like a new batch
            dustParticles.Stop();
            dustParticles.Clear();

            // restart the particle system
            dustParticles.Play();
        }
    }
}


