using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public GameObject groundChunkPrefab;
    public float chunkLength = 20f;
    private float nextSpawnZ = 20f; 
    private float cleanupDistance = 50f;

    void Start()
    {
        SpawnChunk(); 
    }

    void Update()
    {
       
        if (nextSpawnZ < cleanupDistance)
        {
            SpawnChunk();
        }

        CleanupOldChunks();
    }

    void SpawnChunk()
    {
        
        Vector3 spawnPos = new Vector3(0, 0, nextSpawnZ);
        Instantiate(groundChunkPrefab, spawnPos, Quaternion.identity, transform);
        nextSpawnZ += chunkLength;
        Debug.Log("Создан чанк на локальной Z: " + spawnPos.z);
    }

    void CleanupOldChunks()
    {
        foreach (Transform chunk in transform)
        {
            
            if (chunk.localPosition.z < -cleanupDistance)
            {
                Destroy(chunk.gameObject);
            }
        }
    }
}