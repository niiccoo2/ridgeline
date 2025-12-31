using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkSpawner))]
[CanEditMultipleObjects]
public class ChunkSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChunkSpawner spawner = (ChunkSpawner)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Spawn"))
        {
            spawner.SpawnChunk(spawner.testX, spawner.testY);
        }

        if (GUILayout.Button("Destroy all chunks"))
        {
            spawner.DestroyAllChunks();
        }

        if (GUILayout.Button("Spawn all chunks"))
        {
            spawner.SpawnAllChunks(spawner.fullMapHeight, spawner.fullMapWidth, spawner.chunkSize);
        }
    }
}