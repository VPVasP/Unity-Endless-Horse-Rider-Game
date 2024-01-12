using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager instance;
    public int currentObstacleNumber; //obstacle number
    public GameObject obstacle; //obstacle refrence
   [SerializeField] private List<GameObject> spawnedObstacles = new List<GameObject>(); //our list for spawned obstac;es
    private bool obstaclesSpawned = false;
    private GameObject ground;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //we spawn obstacles and we find the ground with the tag ground
        SpawnObstacles(5, new Vector3(0f, -1.5f, 20f), 25f);
        ground = GameObject.FindGameObjectWithTag("Ground");
    }
    // Update is called once per frame
    void Update()
    {
        //we heck if obstacles haven't been spawned and it's the right time to spawn them
        if (!obstaclesSpawned && currentObstacleNumber != 0 && Mathf.Approximately(currentObstacleNumber % 10, 0))
        {
            Debug.Log("Obstacles can now be spawned" + currentObstacleNumber);
            SpawnFromLastPosition(5, 25f);
            obstaclesSpawned = true;
        }
        else
        {
            Debug.Log("currentObstacleNumber: " + currentObstacleNumber);
        }
    }
    //we spawn a obstacles with count for how many we want the starting positiona and the distance between them
    void SpawnObstacles(int count, Vector3 startingPosition, float distanceBetweenObstacles)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obstacles = Instantiate(obstacle, startingPosition + new Vector3(0f, 0f, i * distanceBetweenObstacles), Quaternion.Euler(0f, 90f, 0f));
            spawnedObstacles.Add(obstacles);
        }
    }

    //we spawn obstacles from the last obstacle spawned position and we make the ground bigger on the z axis
    void SpawnFromLastPosition(int count, float distanceBetweenObstacles)
    {
        if (spawnedObstacles.Count > 0)
        {
            Vector3 lastSpawnedPositionObstacles = spawnedObstacles[spawnedObstacles.Count - 1].transform.position;
            SpawnObstacles(count, lastSpawnedPositionObstacles, distanceBetweenObstacles);
            ground.transform.localScale = new Vector3(ground.transform.localScale.x, ground.transform.localScale.y, ground.transform.localScale.y + 50f);

        }
    }
}