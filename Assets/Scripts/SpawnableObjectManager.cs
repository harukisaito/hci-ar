using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObjectManager : MonoBehaviour
{
    public static SpawnableObjectManager Instance;

    public Transform spawnedObjContainer;
    [SerializeField] private GameObject spawnablePrefab;
    // [SerializeField] private GameObject spawnableCubePrefab;
    // [SerializeField] private GameObject spawnableSpherePrefab;
    // [SerializeField] private GameObject spawnableCylinderPrefab;

    private List<SpawnableObject> spawnedObjects = new List<SpawnableObject>();
    private Queue<GameObject> spawnedObjectsQueue = new Queue<GameObject>();

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

    public GameObject SpawnObject(Vector3 pos, Quaternion rot) {
        GameObject spawnedGameobject = GetObject();

        spawnedGameobject.transform.position = pos;
        spawnedGameobject.transform.rotation = rot;

        GameObject spawnedMeshGameObject = spawnedGameobject.transform.GetChild(0).gameObject;
        MeshRenderer spawnedMesh = spawnedMeshGameObject.GetComponent<MeshRenderer>();
        spawnedMesh.material.color = ColorManager.Instance.currentColor;

        spawnedObject = spawnedGameobject.GetComponent<SpawnableObject>();
        spawnedObject.transform.GetChild(0).tag = "Cube";
        spawnedObjects.Add(spawnedObject);
        return spawnedGameobject;
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

    public void ClearAwayObjects() {
        foreach(var obj in spawnedObjects) {
            Destroy(obj);
        }
    }

    public void DeleteObjects() {
        foreach(SpawnableObject obj in spawnedObjects) {
            Destroy(obj.gameObject);
        }

        SaveSystem.DeleteData();
    }

        private GameObject GetObject() {
        GameObject obj;

        if(QueueIsEmpty()) {
            //print("spawning new");
            obj = SpawnNewObject();
        } 
        else {
            //print("getting old");
            obj = GetObjectFromQueue();
        }

        return obj;
    }

    public void DestroyObjectAfterDelay(float delay, GameObject obj) 
    {
        StartCoroutine(DestroyObjectAfterDelayCoroutine(delay, obj));
    }

    public void DestroyObject(GameObject obj) {
        AddObjectToQueue(obj);
    }

    private IEnumerator DestroyObjectAfterDelayCoroutine(float delay, GameObject obj) 
    {
        yield return new WaitForSeconds(delay);
        AddObjectToQueue(obj);
    }
    
    private GameObject SpawnNewObject() {
        GameObject obj = Instantiate(spawnablePrefab, spawnedObjContainer);

        return obj;
    }


    private bool QueueIsEmpty() {
        return spawnedObjectsQueue.Count == 0;
    }

    private void AddObjectToQueue(GameObject obj) {
        obj.SetActive(false);
        spawnedObjectsQueue.Enqueue(obj);
        spawnedObjects.Remove(obj.GetComponent<SpawnableObject>());
    }

    private GameObject GetObjectFromQueue() {
        GameObject obj = spawnedObjectsQueue.Dequeue();
        obj.SetActive(true);
        return obj;
    } 
}
