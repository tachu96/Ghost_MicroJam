using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTransform : MonoBehaviour
{
    public GameObject player;

    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
