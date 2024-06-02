using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval;
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        InvokeRepeating("SpawnObject", 0.0f, spawnInterval);
    }

    void SpawnObject()
    {
        source.Play();
        Instantiate(objectToSpawn, transform.position, transform.rotation, null);
    }
}