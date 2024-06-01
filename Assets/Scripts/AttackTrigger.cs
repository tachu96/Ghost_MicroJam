using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    public MechanicsUI mechanicsUI;

    private void OnTriggerEnter(Collider other)
    {
        Killable killable = other.GetComponent<Killable>();
        Debug.Log("something entered");
        if (killable != null)
        {
            // If it does, call the Kill method on that component
            killable.Kill();
            mechanicsUI.UpdateScore();
        }
    }
}
