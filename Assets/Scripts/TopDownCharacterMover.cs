using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterMover : MonoBehaviour
{

    private InputHandler _input;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private bool rotateTorwardsMouse;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    private void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        var movementVector=MoveTorwardTarget(targetVector);

        if (!rotateTorwardsMouse)
        {
            RotateTorwardMovementVector(movementVector);
        }
        else {
            RotateTorwardsMouseVector();
        }
    }

    private void RotateTorwardsMouseVector()
    {
        Ray ray=camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 1000f)) {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void RotateTorwardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTorwardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0)* targetVector;

        var targetPosition = transform.position + targetVector * speed;

        transform.position = targetPosition;

        return targetVector;
    }
}
