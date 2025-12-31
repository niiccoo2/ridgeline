using System.Collections.Generic;
using GLTFast.Schema;
using Unity.Collections;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public TerrainGenerator terrain; // Removed [SerializeField] - public fields show by default
    public TerrainType[] regions;
    public int testX;
    public int testY;

    public int fullMapWidth;
    public int fullMapHeight;
    public int chunkSize;

    private GameObject chunks;

    private Vector2[] globalOctaveOffsets;

    void OnEnable() {
        if (terrain == null) {
            terrain = GameObject.Find("TerrainManager").GetComponent<TerrainGenerator>();
        }
        if (chunks == null) {
            chunks = new GameObject("chunks");
        }

        // Generate global octave offsets once
        if (globalOctaveOffsets == null) {
            globalOctaveOffsets = Noise.GenerateGlobalOctaveOffsets(terrain.seed, terrain.octaves);
        }
    }

    public void SpawnChunk(int x, int y)
    {   
        if (chunks == null) {
            chunks = new GameObject("chunks"); // Create if not exists
        }

        GameObject thisChunk = new GameObject("chunk_" + x + "_" + y);
        thisChunk.transform.parent = chunks.transform;
        thisChunk.transform.position = new Vector3(x, 0, y); // Set position to connect chunks

        MeshFilter meshFilter = thisChunk.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = thisChunk.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = thisChunk.AddComponent<MeshCollider>();

        terrain.regions = regions;
        
        // Ensure globalOctaveOffsets is up-to-date
        if (globalOctaveOffsets == null || globalOctaveOffsets.Length != terrain.octaves)
        {
            globalOctaveOffsets = Noise.GenerateGlobalOctaveOffsets(terrain.seed, terrain.octaves);
        }

        terrain.GenerateTerrain(new Vector2(x, y), meshRenderer, meshFilter, meshRenderer, meshCollider, globalOctaveOffsets);
    }

    public void SpawnAllChunks(int width, int height, int chunkSize)
    {
        if (chunks == null) {
            chunks = new GameObject("chunks"); // Create if not exists
        }

        DestroyAllChunks();

        terrain.mapWidth = chunkSize;
        terrain.mapHeight = chunkSize;

        for (int x = 0; x < width/chunkSize; x++)
        {
            for (int y = 0; y < height/chunkSize; y++)
            {
                int realX = x*(chunkSize-1);
                int realY = y*(chunkSize-1);

                SpawnChunk(realX, realY);
            }
        }
    }

    public void DestroyAllChunks()
    {
        if (chunks == null) {
            chunks = new GameObject("chunks"); // Create if not exists
        }

        var children = new List<Transform>();
        foreach (Transform child in chunks.transform) {
            children.Add(child);
        }
        foreach (Transform child in children) {
            DestroyImmediate(child.gameObject);
        }
    }
}