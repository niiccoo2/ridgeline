// using Unity.Mathematics;
using UnityEngine;

public class TerrainDisplay : MonoBehaviour
{

    public void DrawTexture(Texture2D texture, Renderer textureRenderer)
    {
        if (textureRenderer.sharedMaterial == null) {  // For DrawMesh
            textureRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        }

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture, MeshFilter meshFilter, MeshRenderer meshRenderer, MeshCollider meshCollider)
    {
        if (meshRenderer.sharedMaterial == null) {
            meshRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        }

        Mesh mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
