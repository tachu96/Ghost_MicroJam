using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    void Start()
    {
        float destroyTime = Random.Range(5f, 10f);

        Destroy(gameObject, destroyTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MainMechanic.Instance.Defeat();
        }
    }
}
