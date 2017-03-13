using UnityEngine;
using System.Collections;

public class ObjectPooling : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize;

    private GameObject[] pool;

    void Start ()
    {   
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++) {
            pool[i] = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
            pool[i].SetActiveRecursively(false);
        }
    }

    public GameObject RetrieveInstance ()
    {
        foreach (GameObject go in pool) {
            if (!go.active) {
                go.SetActiveRecursively(true);
                return go;
            } 
        }

        return null;
    }

    public void DevolveInstance (GameObject go)
    {
        go.SetActiveRecursively(false);
    }
}