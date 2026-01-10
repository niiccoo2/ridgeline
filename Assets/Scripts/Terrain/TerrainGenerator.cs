using UnityEngine;

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
    // public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;
    public float meshHeightMultiplier;

    public GameObject treePrefab;

    // void Start() {
    //     GenerateTerrain();
    // }

    public void GenerateTerrain(Vector2 offset, Renderer textureRenderer, MeshFilter meshFilter, MeshRenderer meshRenderer, MeshCollider meshCollider) {
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
        if (drawMode == DrawMode.NoiseMap) {
            display.DrawTexture(TextureGenarator.TextureFromHeightMap(noiseMap), textureRenderer);
        } else if (drawMode == DrawMode.ColorMap) {
            display.DrawTexture(TextureGenarator.TextureFromColorMap(colorMap, mapWidth, mapHeight), textureRenderer);
        } else if (drawMode == DrawMode.Mesh) {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier), TextureGenarator.TextureFromColorMap(colorMap, mapWidth, mapHeight), meshFilter, meshRenderer, meshCollider);
            SpawnTrees(noiseMap, offset, seed, meshHeightMultiplier, treePrefab, meshFilter.transform);
        }
    }

    public void SpawnTrees(float[,] noiseMap, Vector2 offset, int seed, float heightMultiplier, GameObject treePrefab, Transform chunkParent) {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        System.Random prng = new System.Random(seed*(int)offset[0]+(int)offset[1]);

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) { // z is 2d y
                float y = noiseMap[x, z] * heightMultiplier;
                
                if (prng.Next(100) < 4) { // 4 is 5% bc 0 math
                    float spawnX = x + offset[0];
                    float spawnZ = z + offset[1];

                    Vector3 spawnPosition = new Vector3(spawnX, y, spawnZ);

                    Quaternion spawnRotation = Quaternion.Euler(-90, prng.Next(0,360), 0);
                    var tree = Instantiate(treePrefab, spawnPosition, spawnRotation);
                    tree.transform.parent = chunkParent;
                } 
            }
        }
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