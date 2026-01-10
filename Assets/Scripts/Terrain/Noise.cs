using UnityEngine;
using System.Collections;
// using Unity.Mathematics;

public static class Noise
{
    
    public static float[,] GenerateNoiseMap(
        int mapWidth, int mapHeight, int seed, float scale, int octaves, 
        float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth,mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    // Sample at the actual vertex position to ensure chunk continuity
                    float vertexX = x / (float)(mapWidth - 1) * mapWidth;
                    float vertexY = y / (float)(mapHeight - 1) * mapHeight;
                    
                    float sampleX = (vertexX + offset.x) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (vertexY + offset.y) / scale * frequency + octaveOffsets[i].y;
                    

                    // float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2f - 1f;
                    float perlinValue = Mathf.Sin(sampleX);

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;

            }
        }

        // This normalizes the chunk heights but makes it so that the chunks don't match up due to diffrent max heights
        // for (int y = 0; y < mapHeight; y++)
        // {
        //     for (int x = 0; x < mapWidth; x++)
        //     {
        //         noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
        //     }
        // }

        return noiseMap;
    }
}