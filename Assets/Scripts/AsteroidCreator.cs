using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int poolSize = 20;
    private Queue<GameObject> asteroidPool = new Queue<GameObject>();
    public GameObject player;
    public float asteroidGapYmin;
    public float asteroidGapYmax;
    private float newX;
    private float newY;
    public CameraController camContr;
    private CameraController.CameraBounds bounds;
    /*public struct CameraBounds
    {
        public Vector3 BottomLeft;
        public Vector3 TopRight;
        public Vector3 TopLeft;
        public Vector3 BottomRight;
    }*/
    public Vector3 latestAsteroidPos;
    private Camera mainCamera;
    //private CameraBounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
        // Initialize the object pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefab);
            asteroid.SetActive(false);
            asteroidPool.Enqueue(asteroid);
        }

        SpawnObject(new Vector3(0,5,0));
    }

    // Update is called once per frame
    void Update()
    {
        CreateAsteroid();
    }

    public void CreateAsteroid()
    {
        // only spawn asteroids if player is close enough
        if (DistanceY(latestAsteroidPos, player.transform.position) > 3f)
        {
            return;
        }
        
        //  spawn another asteroid
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        newX = RandomFloatBetween(bounds.BottomLeft.x, bounds.BottomRight.x);
        newY = latestAsteroidPos.y + RandomFloatBetween(asteroidGapYmin,asteroidGapYmax);
        SpawnObject(new Vector3(newX,newY,0));
    }

    public static float DistanceY(Vector3 pos1, Vector3 pos2)
    {
        return Mathf.Abs(pos1.y - pos2.y);
    }

    public static float RandomFloatBetween(float min, float max)
    {
        return Random.Range(min, max);
    }

    // Call this method to spawn the object at a specific position
    public void SpawnObject(Vector3 position)
    {
        if (asteroidPool.Count == 0) return; // Do nothing if the pool is empty

        GameObject asteroid = asteroidPool.Dequeue();
        asteroid.transform.position = position;
        asteroid.SetActive(true);

        latestAsteroidPos = position;
    }
    
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        asteroidPool.Enqueue(obj);
    }

}
