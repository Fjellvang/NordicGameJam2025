using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float jumpForce;
    public float distToGround;
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
        inputActions.Player.Jump.performed += OnJump;
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        input = obj.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        var moveDir = input.y * transform.forward;
        var rotateDir = new Vector3(0, input.x, 0);

        rig.angularVelocity = rotateDir * rotationSpeed;
        rig.linearVelocity = new Vector3(moveDir.x * moveSpeed, rig.linearVelocity.y, moveDir.z * moveSpeed);
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Disable();
    }
}
