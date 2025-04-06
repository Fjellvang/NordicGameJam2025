using UnityEngine;
using UnityEngine.Events;

public class ActionOnCollision : MonoBehaviour
{
    public float MinForceMagnitude;
    public UnityEvent OnCollide;
    void OnCollisionEnter(Collision col)
    {
        Vector3 collisionForce = col.impulse / Time.fixedDeltaTime;
        if (collisionForce.magnitude >= MinForceMagnitude)
        {
            OnCollide?.Invoke();
        }
    }
}
