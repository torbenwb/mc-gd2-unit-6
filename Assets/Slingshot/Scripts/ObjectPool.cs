using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int instanceCount = 10;
    public List<GameObject> instances = new List<GameObject>();
    public int index = 0;

    void Awake()
    {
        SpawnInstances();
    }

    void SpawnInstances()
    {
        for(int i = 0; i < instanceCount; i++)
        {
            GameObject newInstance = Instantiate(prefab);
            newInstance.SetActive(false);
            instances.Add(newInstance);
        }
    }

    public GameObject GetInstance()
    {
        GameObject returnInstance = instances[index++];
        if (index >= instanceCount) index = 0;
        return returnInstance;
    }
}
