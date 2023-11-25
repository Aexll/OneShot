using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_Spawner : MonoBehaviour
{

    public UnityEvent EventSpawn;

    public GameObject spawnable;
    public Transform spawnableTransform;
    public Vector3 spawnSize;

    public void Spawn()
    {
        EventSpawn?.Invoke();
        GameObject spawned;
        if (spawnableTransform != null )
        {
            spawned = Instantiate(spawnable,spawnableTransform);
        }
        else
        {
            spawned = Instantiate(spawnable, transform.position, transform.rotation);
        }
        spawned.transform.localScale = spawnSize;

    }

    public void SetSpawnable(GameObject go)
    {
        spawnable = go;
    }
}
