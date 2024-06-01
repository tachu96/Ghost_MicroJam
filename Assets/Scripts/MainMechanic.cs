using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMechanic : MonoBehaviour
{
    public GameObject AttackTrigger;

    public MechanicsUI mechanicsUI;

    [SerializeField]
    private float AttackDuration = 0.25f;

    [SerializeField]
    private float cooldownDuration = 5f;

    private bool isAttackOnCooldown=false;

    private int originalLayer;
    public int phaserLayer;

    private void Awake()
    {
        originalLayer = gameObject.layer;
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && !isAttackOnCooldown) {
            StartCoroutine(Attack());
        }
        if (Input.GetMouseButton(1))
        {
            gameObject.layer = phaserLayer;
        }
        else { 
            gameObject.layer=originalLayer;
        }
    }

    private IEnumerator Attack()
    {
        AttackTrigger.SetActive(true);

        isAttackOnCooldown = true;

        mechanicsUI.SetAttackCooldownProgress(0f);

        yield return new WaitForSeconds(AttackDuration);

        AttackTrigger.SetActive(false);

        float cooldownTimer = 0f;

        while (cooldownTimer < cooldownDuration)
        {
            cooldownTimer += Time.deltaTime;
            float progress = cooldownTimer / cooldownDuration;
            mechanicsUI.SetAttackCooldownProgress(progress);
            yield return null;
        }

        mechanicsUI.SetAttackCooldownProgress(1f);
        isAttackOnCooldown = false;
    }
}