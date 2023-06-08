using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;
    public int poolSize = 20;
    private List<GameObject> asteroidPool = new List<GameObject>();  // Change queue to list
    public GameObject player;
    public float asteroidGapYmin;
    public float asteroidGapYmax;
    private float newX;
    private float newY;
    public CameraController camContr;
    private CameraController.CameraBounds bounds;
    public Vector3 latestAsteroidPos;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
        // Initialize the object pool
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, asteroidPrefabs.Count); // Get a random index
            GameObject asteroid = Instantiate(asteroidPrefabs[randomIndex]); // Instantiate a random asteroid prefab
            asteroid.SetActive(false);
            asteroidPool.Add(asteroid); // Add it to the list
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

        int randomIndex = Random.Range(0, asteroidPool.Count); // Get a random index
        GameObject asteroid = asteroidPool[randomIndex]; // Get a random asteroid from the pool
        asteroidPool.RemoveAt(randomIndex); // Remove this asteroid from the pool
        asteroid.transform.position = position;
        asteroid.SetActive(true);

        latestAsteroidPos = position;
    }
    
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        asteroidPool.Add(obj); // Add it back to the pool
    }
}
