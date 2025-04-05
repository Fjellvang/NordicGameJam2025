using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    Rigidbody rig;
    Vector2 input;
    InputActions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        input = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        var moveDir = input.y * transform.forward;
        var rotateDir = new Vector3(0, input.x, 0);

        var linearVel = new Vector3(moveDir.x * moveSpeed, rig.linearVelocity.y, moveDir.z * moveSpeed);
        var angularVel = rotateDir * rotationSpeed;

        rig.angularVelocity = angularVel;
        rig.linearVelocity = linearVel;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }
}
