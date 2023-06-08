using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCreator
 : MonoBehaviour
{
    public List<GameObject> dustPrefabs;
    public int poolSize = 20;
    private List<GameObject> dustPool = new List<GameObject>(); 
    public GameObject player;
    public float dustGapYmin;
    public float dustGapYmax;
    private float newX;
    private float newY;
    public CameraController camContr;
    private CameraController.CameraBounds bounds;
    public Vector3 latestdustPos;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
        // Initialize the object pool
        for (int i = 0; i < poolSize; i++)
        {
            //int randomIndex = Random.Range(0, dustPrefabs.Count);
            GameObject dust = Instantiate(dustPrefabs[0]);
            dust.SetActive(false);
            dustPool.Add(dust);
        }

        SpawnObject(new Vector3(0,5,0));
    }

    // Update is called once per frame
    void Update()
    {
        Createdust();
    }

    public void Createdust()
    {
        
        //  spawn another dust
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        float dustSize = dustPool[0].GetComponent<Renderer>().bounds.size.x;  // Calculate the size of the dust

        newX = RandomFloatBetween(bounds.BottomLeft.x + dustSize/2, bounds.BottomRight.x - dustSize/2);  // Subtract/add half the dust size from the bounds
        newY = latestdustPos.y + RandomFloatBetween(dustGapYmin,dustGapYmax);
        SpawnObject(new Vector3(newX,newY,0));
    }


    private float RandomFloatBetween(float min, float max)
    {
        return Random.Range(min, max);
    }

    // Call this method to spawn the object at a specific position
    public void SpawnObject(Vector3 position)
    {
        if (dustPool.Count == 0) return; 

        int randomIndex = Random.Range(0, dustPool.Count); 
        GameObject dust = dustPool[randomIndex]; 
        dustPool.RemoveAt(randomIndex); 
        dust.transform.position = position;
        dust.SetActive(true);
        latestdustPos = position;
    }
    
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        dustPool.Add(obj); // Add it back to the pool
    }

    private void OnBecameInvisible()
    {
        // Return this asteroid to the pool when it's no longer visible
        ReturnObjectToPool(this.gameObject);

    }
}
