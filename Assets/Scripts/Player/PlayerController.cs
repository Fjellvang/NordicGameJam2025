using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public ParticleSystem dustEmitter;
    public float minLandingVelocityForDust;
    public int particlesEmittedWhenLanding;

    public Vector3 lowerRayOriginOffset;
    public float stepHeight; // Difference in height between lower and upper ray
    public float lowerStepRayLength;
    public float upperStepRayLength; // Upper ray should be a bit longer
    public float stepJumpForce; // How much force to push the player with, when its going up a step

    public bool OnGround => onGround;
    public bool IsMoving => rig.linearVelocity.sqrMagnitude > 0.01f;
    
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
    public event Action OnInteractPerformed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Interact.performed += OnInteract;

        dustEmitter.Stop();
    }

    public void OnInteract(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke();
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        input = obj.ReadValue<Vector2>();
        
        // Dust particles
        if (input == Vector2.zero || onGround == false)
        {
            dustEmitter.Stop();
        } else
        {
            dustEmitter.Play();
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (onGround)
        {
            var jumpDir = transform.forward + Vector3.up;
            rig.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
            dustEmitter.Stop();
        }
    }

    private void FixedUpdate()
    {
        // Check if cat just landed on ground and velocity.
        var onGroundLastUpdate = onGround;
        
        onGround = Physics.Raycast(transform.position, Vector3.down, distToGround);

        if (onGround == true && onGroundLastUpdate == false)
        {
            if (rig.linearVelocity.y < minLandingVelocityForDust)
            {
                dustEmitter.Emit(particlesEmittedWhenLanding);
            }
        }


        // Stepping up slopes

        var lowerRayOrigin = transform.position + (transform.forward * lowerRayOriginOffset.z) + (transform.up * lowerRayOriginOffset.y) + (transform.right * lowerRayOriginOffset.x);
        bool lowerHit = Physics.Raycast(lowerRayOrigin, transform.forward, lowerStepRayLength);
        var upperRayOrigin = lowerRayOrigin + Vector3.up * stepHeight;
        if (lowerHit)
        {
            bool upperHit = Physics.Raycast(upperRayOrigin, transform.forward, upperStepRayLength);
            
            if (!upperHit)
            {
                rig.AddForce(Vector3.up * stepJumpForce, ForceMode.Impulse);
            }
        }
        
        // Rotation

        var rotateDir = new Vector3(0, input.x, 0);
        var goalTorque= rotateDir * rotationSpeed;
        var torqueDiff = goalTorque - rig.angularVelocity;

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


    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Disable();
    }
}
