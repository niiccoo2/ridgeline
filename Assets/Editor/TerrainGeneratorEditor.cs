using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
[CanEditMultipleObjects]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator terGen = (TerrainGenerator)target;

        if (DrawDefaultInspector())
        {
            if (terGen.autoUpdate)
            {
                terGen.GenerateTerrain();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            terGen.GenerateTerrain();
        }
    }
}
