using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(SpawnableObject[] spawnableObjects) {
        SpawnableObjectDataSets data = Convert(spawnableObjects);

        string path;

        #if UNITY_EDITOR
            path = Path.Combine(Application.dataPath, "spawnableData.json");
        #else 
            path = Path.Combine(Application.persistentDataPath, "spawnableData.json");
        #endif


        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }

    private static SpawnableObjectDataSets Convert(SpawnableObject[] data) {
        SpawnableObjectData[] spawnableObjects = new SpawnableObjectData[data.Length];

        int index = 0;
        foreach(var dataSet in data) {
            spawnableObjects[index] = new SpawnableObjectData(dataSet);
            index++;
        }

        SpawnableObjectDataSets spawnableObjectDataSets = new SpawnableObjectDataSets(spawnableObjects);

        return spawnableObjectDataSets;
    }

    public static SpawnableObjectDataSets LoadData() {
        SpawnableObjectDataSets data;

        string path;

        #if UNITY_EDITOR
            path = Path.Combine(Application.dataPath, "spawnableData.json");
        #else 
            path = Path.Combine(Application.persistentDataPath, "spawnableData.json");
        #endif

        string json = File.ReadAllText(path);

        data = JsonUtility.FromJson<SpawnableObjectDataSets>(json);

        return data;
    }

    public static void DeleteData() {
        string path;

        #if UNITY_EDITOR
            path = Path.Combine(Application.dataPath, "spawnableData.json");
        #else 
            path = Path.Combine(Application.persistentDataPath, "spawnableData.json");
        #endif

        File.Delete(path);
    }
}
