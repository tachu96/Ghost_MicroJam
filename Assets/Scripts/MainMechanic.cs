using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(TopDownCharacterMover))]
public class MainMechanic : MonoBehaviour
{
    public GameObject AttackTrigger;

    public MechanicsUI mechanicsUI;

    public float phaseSpeedUpFactor;

    public bool performingAttack=false;

    [SerializeField]
    private float AttackDuration = 0.25f;

    [SerializeField]
    private float attackCooldownDuration = 5f;

    [SerializeField]
    private float phaseDuration = 0.25f;

    [SerializeField]
    private float phaseCooldownDuration = 5f;

    private bool isAttackOnCooldown=false;
    private bool isPhaseOnCooldown= false;

    private int originalLayer;
    public int phaserLayer;

    private TopDownCharacterMover topDownCharacterMover;

    public Animator animator;

    public SkinnedMeshRenderer ghostMesh;
    public Material originalMaterial;
    public Material phaseMaterial;

    private void Awake()
    {
        originalLayer = gameObject.layer;
        topDownCharacterMover=GetComponent<TopDownCharacterMover>();
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && !isAttackOnCooldown) {
            animator.SetBool("Attacking", true);
            StartCoroutine(Attack());
        }
        if (Input.GetMouseButton(1) && !isPhaseOnCooldown)
        {
            ghostMesh.material=phaseMaterial;
            StartCoroutine(Phase());
        }
    }

    private IEnumerator Attack()
    {
        AttackTrigger.SetActive(true);

        isAttackOnCooldown = true;

        mechanicsUI.SetAttackCooldownProgress(0f);

        performingAttack=true;

        topDownCharacterMover.forwardThrust();

        yield return new WaitForSeconds(AttackDuration);

        animator.SetBool("Attacking", false);

        performingAttack = false;

        topDownCharacterMover.Break();

        AttackTrigger.SetActive(false);

        float cooldownTimer = 0f;

        while (cooldownTimer < attackCooldownDuration)
        {
            cooldownTimer += Time.deltaTime;
            float progress = cooldownTimer / attackCooldownDuration;
            mechanicsUI.SetAttackCooldownProgress(progress);
            yield return null;
        }

        mechanicsUI.SetAttackCooldownProgress(1f);
        isAttackOnCooldown = false;
    }

    private IEnumerator Phase()
    {
        gameObject.layer = phaserLayer;

        isPhaseOnCooldown = true;

        topDownCharacterMover.TemporalSpeedUp(phaseSpeedUpFactor);

        mechanicsUI.SetPhaseCooldownProgress(0f);

        yield return new WaitForSeconds(phaseDuration);

        ghostMesh.material = originalMaterial;

        gameObject.layer = originalLayer;

        topDownCharacterMover.ReturnToNormalSpeed();

        float cooldownTimer = 0f;

        while (cooldownTimer < phaseCooldownDuration)
        {
            cooldownTimer += Time.deltaTime;
            float progress = cooldownTimer / phaseCooldownDuration;
            mechanicsUI.SetPhaseCooldownProgress(progress);
            yield return null;
        }

        mechanicsUI.SetPhaseCooldownProgress(1f);
        isPhaseOnCooldown = false;
    }

    public void SpeedBuff() {
        topDownCharacterMover.SpeedBuff();
    }

    public void AttackCooldownBuff()
    {
        if (attackCooldownDuration >= 0.1f) {
            attackCooldownDuration -= 0.25f;
        }
        else {
            attackCooldownDuration = 0.1f;
        }
    }

    internal void phaseDurationBuff()
    {
        if (phaseDuration < 5f)
        {
            phaseDuration += 0.25f;
        }
        else {
            phaseDuration = 5f;
        }
    }
}