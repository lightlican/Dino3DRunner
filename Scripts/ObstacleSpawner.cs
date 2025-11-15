using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnDistance = 30f;

    private float timer = 0f;
    private float currentSpawnRate;

    void Update()
    {
        currentSpawnRate = GameManager.Instance.GetSpawnSpeed();
        timer += Time.deltaTime;

        if (timer >= currentSpawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs != null && obstaclePrefabs.Length > 0)
        {
            GameObject randomObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            
            Vector3 spawnPos = new Vector3(0, 3f, spawnDistance);

            
            Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, 0f);

            GameObject newObstacle = Instantiate(randomObstacle, spawnPos, spawnRotation);

            
            Rigidbody rb = newObstacle.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(0, 0, -GameManager.Instance.GetWorldSpeed());
            }
        }
    }
}