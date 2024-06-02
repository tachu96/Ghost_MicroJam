using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuffPlayer : MonoBehaviour
{
    public BuffType buffType;

    private Crowd crowdScript;

    private void Awake()
    {
        crowdScript = GetComponent<Crowd>();
    }

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
                    case BuffType.phaseDuration:
                        mainMechanic.phaseDurationBuff();
                        break;
                    case BuffType.AttackDuration:
                        mainMechanic.attackDurationBuff();
                        break;
                    case BuffType.phaseCooldownDuration:
                        mainMechanic.phaseCooldownDurationBuff();
                        break;
                }
                Debug.Log("gave buff!");
            }
            else
            {
                Debug.LogWarning("MainMechanic component not found on the player.");
            }
            crowdScript.resetTarget();
            Destroy(this.gameObject);
        }
    }
}
