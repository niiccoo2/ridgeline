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
    TerrainGenerator.DrawMode drawMode = TerrainGenerator.DrawMode.Mesh;

    private GameObject chunks;

    void Start() {
        if (terrain == null) {
            terrain = GameObject.Find("TerrainManager").GetComponent<TerrainGenerator>();
        }
        chunks = new GameObject("chunks");
    }

    public void SpawnChunk(int x, int y)
    {   
        if (chunks == null) {
            chunks = new GameObject("chunks"); // Create if not exists
        }

        GameObject thisChunk = new GameObject("chunk_" + x + "_" + y);
        thisChunk.transform.parent = chunks.transform;

        // Renderer textureRenderer = thisChunk.AddComponent<Renderer>();
        MeshFilter meshFilter = thisChunk.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = thisChunk.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = thisChunk.AddComponent<MeshCollider>();

        terrain.regions = regions;
        
        terrain.GenerateTerrain(new Vector2(x, y), meshRenderer, meshFilter, meshRenderer, meshCollider);
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