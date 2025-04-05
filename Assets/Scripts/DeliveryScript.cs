using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeliveryScript : MonoBehaviour
{
    public PickupType expectedPickupType;

    [Tooltip("Amout of sleep time to add when delivery is successful")]
    public float SleepValue = 5f;
    public event Action OnDeliveryCompleted;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pickup>(out var pickup))
        {
            if (pickup.PickupType == expectedPickupType)
            {
                // Handle successful delivery
                Debug.Log("Delivery successful!");
                SleepManager.Instance.AddSleepTime(SleepValue);
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
