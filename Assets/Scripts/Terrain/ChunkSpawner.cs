using System;
using System.Collections.Generic;
using GLTFast.Schema;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ChunkSpawner : MonoBehaviour
{
    public TerrainGenerator terrain;
    public TerrainType[] regions;
    public int testX;
    public int testY;

    public int fullMapWidth;
    public int fullMapHeight;
    public int chunkSize;

    private GameObject chunks;

    void OnEnable() {
        if (terrain == null) {
            terrain = GameObject.Find("TerrainManager").GetComponent<TerrainGenerator>();
        }
        if (chunks == null) {
            chunks = new GameObject("chunks");
        }
    }

    private void MakeSureChunksExists()
    {
        if (chunks == null) {
            chunks = new GameObject("chunks"); // Create if not exists
        }
    }

    public void SpawnChunk(int x, int y)
    {   
        MakeSureChunksExists();

        GameObject thisChunk = new GameObject("chunk_" + x + "_" + y);
        thisChunk.transform.parent = chunks.transform;
        thisChunk.transform.position = new Vector3(x, 0, y); // Set position to connect chunks

        MeshFilter meshFilter = thisChunk.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = thisChunk.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = thisChunk.AddComponent<MeshCollider>();

        terrain.regions = regions;

        terrain.GenerateTerrain(new Vector2(x, y), meshRenderer, meshFilter, meshRenderer, meshCollider);
        
        thisChunk.SetActive(false);
    }

    public void SpawnAllChunks(int width, int height, int chunkSize)
    {
        MakeSureChunksExists();

        DestroyAllChunks();

        terrain.mapWidth = chunkSize;
        terrain.mapHeight = chunkSize;

        for (int x = 0; x < width/chunkSize; x++)
        {
            for (int y = 0; y < height/chunkSize; y++)
            {
                int realX = x * chunkSize;
                int realY = y * chunkSize;

                SpawnChunk(realX, realY);
            }
        }
    }

    public void DestroyAllChunks()
    {
        MakeSureChunksExists();

        var children = new List<Transform>();
        foreach (Transform child in chunks.transform) {
            children.Add(child);
        }
        foreach (Transform child in children) {
            DestroyImmediate(child.gameObject);
        }
    }

    private void EnableChunk(int x, int y, bool enable = true)
    {
        MakeSureChunksExists();

        Transform chunkTransform = chunks.transform.Find("chunk_" + x + "_" + y);
        GameObject chunk;
        if (chunkTransform != null)
        {
            chunk = chunkTransform.gameObject;
        } else
        {
            return;
        }

        chunk.SetActive(enable);
    }

    private void DisableChunk(int x, int y)
    {
        EnableChunk(x, y, false);
    }

    public void SpawnNearChunks(int x, int y, int numberOfChunks = 10)
    {
        // Calculate the current chunk index (not world position)
        int currentChunkX = Mathf.FloorToInt((float)x / chunkSize);
        int currentChunkY = Mathf.FloorToInt((float)y / chunkSize);

        // Create a 2D array for all chunks
        int chunkGridWidth = fullMapWidth / chunkSize;
        int chunkGridHeight = fullMapHeight / chunkSize;

        // Iterate over all chunks in the grid
        for (int chunkX = 0; chunkX < chunkGridWidth; chunkX++)
        {
            for (int chunkY = 0; chunkY < chunkGridHeight; chunkY++)
            {
                // Calculate the distance in chunk indices
                int distanceX = Mathf.Abs(chunkX - currentChunkX);
                int distanceY = Mathf.Abs(chunkY - currentChunkY);

                // Mark chunks within the specified range
                if (distanceX < numberOfChunks && distanceY < numberOfChunks)
                {
                    EnableChunk(chunkX * chunkSize, chunkY * chunkSize);
                } else
                {
                    DisableChunk(chunkX * chunkSize, chunkY * chunkSize);
                }
            }
        }
    }
}