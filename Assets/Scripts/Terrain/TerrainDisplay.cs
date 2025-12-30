// using Unity.Mathematics;
using UnityEngine;

public class TerrainDisplay : MonoBehaviour
{

    public void DrawTexture(Texture2D texture, Renderer textureRenderer)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture, MeshFilter meshFilter, MeshRenderer meshRenderer, MeshCollider meshCollider)
    {
        Mesh mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
