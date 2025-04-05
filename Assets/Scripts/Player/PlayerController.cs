using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float maxSpeed;
    public float rotationSpeed;
    public float jumpForce;
    public float distToGround;
    bool onGround;
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
        if (onGround)
        {
            var jumpDir = transform.forward + Vector3.up;
            rig.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, distToGround);

        Vector3 moveDir = input.y * transform.forward;
        var goalVelocity = moveDir * maxSpeed;
        var velocityDiff = goalVelocity - rig.linearVelocity;
        rig.AddForce(velocityDiff, ForceMode.Force);
    }

    private void Update()
    {
        var rotateDir = new Vector3(0, input.x, 0);
        rig.angularVelocity = rotateDir * rotationSpeed;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Disable();
    }
}
