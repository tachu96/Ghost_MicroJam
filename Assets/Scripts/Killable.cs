using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public GameObject[] ghostType;
    private GameObject ghostToSpawn;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    private Crowd crowdScript;

    public GameObject smokeVFX;

    public Material[] bodyMaterials;
    public Material headMaterial;
    private void Awake()
    {
        crowdScript = GetComponent<Crowd>();

        if (ghostType.Length > 0) { 
            int randomIndex= Random.Range(0, ghostType.Length);
            ghostToSpawn = ghostType[randomIndex];

            Material[] materials = new Material[] { bodyMaterials[randomIndex], headMaterial};
            skinnedMeshRenderer.materials = materials;
        }
    }
    public void Kill()
    {
        crowdScript.resetTarget();
        Instantiate(smokeVFX, transform.position, transform.rotation, null);

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
