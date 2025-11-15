using UnityEngine;
using System.Collections;

public class BiomeManager : MonoBehaviour
{
    public static BiomeManager Instance;

    [System.Serializable]
    public class Biome
    {
        public string name;
        public Material groundMaterial;
        public Color skyColor;
        public GameObject[] decorationPrefabs;
    }

    public Biome[] biomes;
    public float biomeChangeInterval = 30f; 
    private float timer = 0f;
    private int currentBiomeIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        SwitchToRandomBiome();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= biomeChangeInterval)
        {
            SwitchToRandomBiome();
            timer = 0f;
        }
    }

    void SwitchToRandomBiome()
    {
        int newBiomeIndex;
        do
        {
            newBiomeIndex = Random.Range(0, biomes.Length);
        }
        while (newBiomeIndex == currentBiomeIndex && biomes.Length > 1);

        currentBiomeIndex = newBiomeIndex;
        ApplyBiome(biomes[currentBiomeIndex]);

        Debug.Log("Биом изменен на: " + biomes[currentBiomeIndex].name);
    }

    void ApplyBiome(Biome biome)
    {
        
        GameObject ground = GameObject.FindWithTag("Ground");
        if (ground != null && biome.groundMaterial != null)
        {
            ground.GetComponent<Renderer>().material = biome.groundMaterial;
        }

       
        Camera.main.backgroundColor = biome.skyColor;

        
    }

    
    public void ChangeBiomeTest()
    {
        SwitchToRandomBiome();
    }
}