using UnityEngine;
using System.Collections;

// public class TerrainGenerator : MonoBehaviour
// {
//     public int seed = 12345;
//     public int width = 256;
//     public int length = 256;
//     public float scale = 20f;
//     public float heightMultiplier = 1f;

//     void Start()
//     {
//         GenerateTerrain();
//     }

//     void GenerateTerrain()
//     {
//         System.Random prng = new System.Random(seed);
//         float offsetX = (float)prng.NextDouble() * 10000f;
//         float offsetZ = (float)prng.NextDouble() * 10000f;

//         float[,] heights = new float[width, length];

//         for (int x = 0; x < width; x++)
//         {
//             for (int z = 0; z < length; z++)
//             {
//                 float sampleX = x / scale + offsetX;
//                 float sampleZ = z / scale + offsetZ;

//                 // multi-layered noise for realism
//                 float y = Mathf.PerlinNoise(sampleX, sampleZ) * 0.6f +
//                           Mathf.PerlinNoise(sampleX*2, sampleZ*2) * 0.3f +
//                           Mathf.PerlinNoise(sampleX*4, sampleZ*4) * 0.1f;

//                 heights[x, z] = y;
//             }
//         }

//         Terrain terrain = GetComponent<Terrain>();
//         terrain.terrainData.heightmapResolution = width + 1;
//         terrain.terrainData.size = new Vector3(width, heightMultiplier, length);
//         terrain.terrainData.SetHeights(0, 0, heights);
//     }
// }

public class TerrainGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoUpdate;

    public void GenerateTerrain()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        TerrainDisplay display = FindAnyObjectByType<TerrainDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}