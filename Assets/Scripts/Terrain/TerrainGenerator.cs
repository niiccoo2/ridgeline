using UnityEngine;
using System.Collections;
using System.Diagnostics.Contracts;

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
    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateTerrain()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {   
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        TerrainDisplay display = FindAnyObjectByType<TerrainDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenarator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenarator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        // } else if (drawMode == DrawMode.Mesh)
        // {
        //     display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), );
        // }
        
    }

    void OnValidate()
    {
        if (mapWidth < 1) mapWidth = 1;

        if (mapHeight < 1) mapHeight = 1;

        if (lacunarity < 1) lacunarity = 1;

        if (octaves < 0) octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}