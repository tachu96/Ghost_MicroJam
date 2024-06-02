using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivator : MonoBehaviour
{
    public List<GameObject> objectsToActivate; 
    public float activationInterval = 10.0f;

    void Start()
    {
        // Start invoking the ActivateRandomObject method repeatedly
        InvokeRepeating("ActivateRandomObject", 0.0f, activationInterval);
    }

    void ActivateRandomObject()
    {
        if (objectsToActivate.Count > 0)
        {
            int randomIndex = Random.Range(0, objectsToActivate.Count);
            GameObject obj = objectsToActivate[randomIndex];

            float randomYRotation = Random.Range(0f, 360f);
            obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.eulerAngles.x, randomYRotation, obj.transform.rotation.eulerAngles.z);

            obj.SetActive(true);

            objectsToActivate.RemoveAt(randomIndex);
        }
        else
        {
            CancelInvoke("ActivateRandomObject");
        }
    }
}
