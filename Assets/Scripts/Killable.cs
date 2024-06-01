using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public void Kill()
    {
        // Implement the kill logic here
        Debug.Log($"{gameObject.name} has been killed!");

        Destroy(gameObject);
    }
}
