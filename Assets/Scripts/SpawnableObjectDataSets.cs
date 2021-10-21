using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObjectDataSets
{
    public SpawnableObjectData[] spawnableObjectDatasets;

    public SpawnableObjectDataSets(SpawnableObjectData[] spawnableObjectDatasets) {
        this.spawnableObjectDatasets = spawnableObjectDatasets;
    }
}
