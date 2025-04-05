using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float maxSpeed;
    public float brakeFactor;
    public float inAirSpeedFactor;
    public float rotationSpeed;
    public float jumpForce;
    public float distToGround;
    public AnimationCurve TurnAccelCurve;
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

        // Rotation

        var rotateDir = new Vector3(0, input.x, 0);
        var goalRotation = rotateDir * rotationSpeed;
        var torqueDiff = goalRotation - rig.angularVelocity;

        rig.AddTorque(torqueDiff);

        // Movement 

        Vector3 moveDir = input.y * transform.forward;
        var goalVelocity = moveDir * maxSpeed;
        var velocityDiff = (goalVelocity - rig.linearVelocity);
        
        
        if (input.y == 0 && onGround)
        {
            velocityDiff *= brakeFactor;
        }
        if (!onGround)
        {
            velocityDiff *= inAirSpeedFactor;
        }

        // The curve is designed to make it brake faster when doing a 180 turn so its more responsive.
        velocityDiff *= TurnAccelCurve.Evaluate(Vector3.Dot(rig.linearVelocity.normalized, moveDir));

        rig.AddForce(velocityDiff, ForceMode.Acceleration);
        
    }

    private void Update()
    {
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Disable();
    }
}
