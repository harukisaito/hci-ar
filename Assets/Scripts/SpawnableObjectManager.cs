using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObjectManager : MonoBehaviour
{
    public static SpawnableObjectManager Instance;

    [SerializeField] private GameObject spawnablePrefab;

    private List<SpawnableObject> spawnedObjects = new List<SpawnableObject>();
    private SpawnableObject spawnedObject;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        spawnedObject = null;
    }

    public void SpawnObject(Vector3 pos, Quaternion rot) {
        GameObject spawnedGameobject = 
            Instantiate(spawnablePrefab, pos, rot, transform);
        spawnedObject = spawnedGameobject.GetComponent<SpawnableObject>();
        spawnedObjects.Add(spawnedObject);
    }

    public void SaveObjects() {
        SaveSystem.SaveData(spawnedObjects.ToArray());
    }

    public void LoadObjects() {
        SpawnableObjectDataSets data = SaveSystem.LoadData();

        foreach(var obj in data.spawnableObjectDatasets) {
            Vector3 pos = new Vector3(
                obj.position.x,
                obj.position.y,
                obj.position.z
            );

            Quaternion rot = new Quaternion(
                obj.rotation.w,
                obj.rotation.x,
                obj.rotation.y,
                obj.rotation.z
            );

            SpawnObject(pos, rot);
        }
    }

    public void DeleteObjects() {
        SaveSystem.DeleteData();
    }

}
