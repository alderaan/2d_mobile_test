using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float initialSpeed = 1.0f;
    public float speedIncreasePerSecond = 0.1f;
    public float currentSpeed;
    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
        //mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = initialSpeed + speedIncreasePerSecond * Time.time;
        transform.position += new Vector3(0, currentSpeed * Time.deltaTime, 0);
    }
}
