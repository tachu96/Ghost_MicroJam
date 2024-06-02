using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuffPlayer : MonoBehaviour
{
    public enum BuffType
    {
        Speed,
        AttackCooldown,
    }

    public BuffType buffType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainMechanic mainMechanic = other.GetComponent<MainMechanic>();
            if (mainMechanic != null)
            {
                switch (buffType)
                {
                    case BuffType.Speed:
                        mainMechanic.SpeedBuff();
                        break;
                    case BuffType.AttackCooldown:
                        mainMechanic.AttackCooldownBuff();
                        break;
                }
                Debug.Log("gave buff!");
            }
            else
            {
                Debug.LogWarning("MainMechanic component not found on the player.");
            }

            Destroy(this.gameObject);
        }
    }
}
