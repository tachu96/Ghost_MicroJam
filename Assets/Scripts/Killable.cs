using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public GameObject[] ghostType;
    private GameObject ghostToSpawn;

    private void Awake()
    {
        if (ghostType.Length > 0) { 
            int randomIndex= Random.Range(0, ghostType.Length);
            ghostToSpawn = ghostType[randomIndex];
        }
    }
    public void Kill()
    {
        if (ghostToSpawn != null)
        {
            float offsetX = Random.Range(-1.0f, 1.0f);
            float offsetZ = Random.Range(-1.0f, 1.0f);
            Vector2 normalizedRandom= new Vector2(offsetX, offsetZ).normalized;
            Vector3 newPosition = new Vector3(transform.position.x + normalizedRandom.x*2f, transform.position.y, transform.position.z + normalizedRandom.y*2f);
            Instantiate(ghostToSpawn, newPosition, transform.rotation, null);
            Destroy(gameObject);
        }
        else { 
            Destroy(gameObject);
            Debug.LogWarning("No ghost prefab");
        }

    }
}
