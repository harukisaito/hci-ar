using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObjectData
{
    public Position position;
    public Rotation rotation;

    public SpawnableObjectData(SpawnableObject spawnableObject) {
        Vector3 pos = spawnableObject.transform.position;
        Quaternion rot = spawnableObject.transform.rotation;

        position = new Position(pos.x, pos.y, pos.z);
        rotation = new Rotation(rot.w, rot.x, rot.y, rot.z);
    }
}

[System.Serializable]
public class Position {
    public float x;
    public float y;
    public float z;

    public Position(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

[System.Serializable]
public class Rotation {
    public float w;
    public float x;
    public float y;
    public float z;

    public Rotation(float w, float x, float y, float z) {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
