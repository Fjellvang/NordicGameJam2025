using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeliveryScript : MonoBehaviour
{
    public PickupType expectedPickupType;
    public event Action OnDeliveryCompleted;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pickup>(out var pickup))
        {
            if (pickup.PickupType == expectedPickupType)
            {
                // Handle successful delivery
                Debug.Log("Delivery successful!");
                OnDeliveryCompleted?.Invoke();
                Destroy(pickup.gameObject);
            }
            else
            {
                // Handle incorrect item
                Debug.Log("Incorrect item delivered.");
            }
        }
    }
}
