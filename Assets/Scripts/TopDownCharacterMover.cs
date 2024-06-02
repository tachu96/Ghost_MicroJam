using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterMover : MonoBehaviour
{

    private InputHandler _input;
    [SerializeField]
    private float originalMoveSpeed;

    public float currentMoveSpeed;
    private bool performingPhase=false;
    public float moveSpeedHolder;

    [SerializeField]
    private float forwardThrustIncrease;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float rotateSpeed;

    private Rigidbody _rb;

    private MainMechanic _mainMechanic;

    [SerializeField]
    private float speedBuff;


    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _rb = GetComponent<Rigidbody>();
        currentMoveSpeed = originalMoveSpeed;
        moveSpeedHolder = currentMoveSpeed;
        _mainMechanic = GetComponent<MainMechanic>();
    }

    private void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        var movementVector=MoveTorwardTarget(targetVector);

        RotateTorwardsMouseVector();
    }

    private void RotateTorwardsMouseVector()
    {
        Ray ray=camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 1000f)) {
            var target = hitInfo.point;
            target.y = transform.position.y;
            if (!_mainMechanic.performingAttack) { 
                transform.LookAt(target);
            }
        }
    }


    private Vector3 MoveTorwardTarget(Vector3 targetVector)
    {
        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;

        var velocity = targetVector.normalized * currentMoveSpeed;
        if (!_mainMechanic.performingAttack)
        {
            _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        }

        return targetVector;
    }

    public void TemporalSpeedUp(float movementIncrease) {
        moveSpeedHolder = currentMoveSpeed;
        currentMoveSpeed =currentMoveSpeed * movementIncrease;
        performingPhase = true;
    }

    public void ReturnToNormalSpeed()
    {
        currentMoveSpeed = moveSpeedHolder;
        performingPhase = false;
    }

    public void forwardThrust() {
        _rb.velocity = Vector3.zero;
        _rb.AddForce(transform.forward*moveSpeedHolder*forwardThrustIncrease,ForceMode.Impulse);
    }

    public void Break()
    {
        _rb.velocity = Vector3.zero;
    }

    public void SpeedBuff()
    {
        moveSpeedHolder += speedBuff;
        if (!performingPhase) { 
            currentMoveSpeed=moveSpeedHolder;
        }
    }
}
