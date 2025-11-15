using UnityEngine;
using System.Collections.Generic;

public class MovingWorld : MonoBehaviour
{
    [System.Serializable]
    public class Biome
    {
        public GameObject groundPrefab;
        public Material skyboxMaterial;
        public string name;
    }

    public Biome[] biomes;
    public float worldSpeed = 5f;
    public float chunkLength = 756f; 

    private List<GameObject> activeChunks = new List<GameObject>();
    private int currentBiomeIndex = 0;
    private int chunksSpawnedInBiome = 0;

    void Start()
    {
        
        SpawnChunk(0f);      
        SpawnChunk(756f);    
    }

    void Update()
    {
        
        foreach (GameObject chunk in activeChunks)
        {
            chunk.transform.Translate(-Vector3.forward * worldSpeed * Time.deltaTime);
        }

       
        if (activeChunks.Count > 0)
        {
            GameObject firstChunk = activeChunks[0];

            
            if (firstChunk.transform.position.z < -200f)
            {
                
                Destroy(firstChunk);
                activeChunks.RemoveAt(0);

                
                GameObject lastChunk = activeChunks[activeChunks.Count - 1];
                float newChunkZ = lastChunk.transform.position.z + chunkLength;
                SpawnChunk(newChunkZ);
            }
        }
    }

    void SpawnChunk(float spawnZ)
    {
        GameObject newChunk = Instantiate(biomes[currentBiomeIndex].groundPrefab);
        newChunk.transform.position = new Vector3(0, 0, spawnZ);
        activeChunks.Add(newChunk);

        chunksSpawnedInBiome++;

        Debug.Log($"Создан чанк {chunksSpawnedInBiome} биома '{biomes[currentBiomeIndex].name}' на Z: {spawnZ}");

       
        if (chunksSpawnedInBiome >= 2)
        {
            SwitchToNextBiome();
        }
    }

    void SwitchToNextBiome()
    {
        currentBiomeIndex = (currentBiomeIndex + 1) % biomes.Length;
        chunksSpawnedInBiome = 0;
        RenderSettings.skybox = biomes[currentBiomeIndex].skyboxMaterial;
        Debug.Log($"Переключен на биом: {biomes[currentBiomeIndex].name}");
    }
}