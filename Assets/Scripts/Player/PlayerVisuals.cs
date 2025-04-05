using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public Animator Animator;
    public PlayerController Controller;
    
    bool isMoving;
    bool isInAir;

    private void Update()
    {
        Animator.SetBool("IsMoving", isMoving);
        Animator.SetBool("IsInAir", isInAir);
    }
}
