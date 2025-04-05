using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public Animator Animator;
    public PlayerController Controller;
    
    private void Update()
    {
        Animator.SetBool("IsMoving", Controller.IsMoving);
        Animator.SetBool("IsInAir", !Controller.OnGround);
    }
}
