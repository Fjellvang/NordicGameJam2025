using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DeliveryScript : MonoBehaviour
{
    public PickupType expectedPickupType;

    [Tooltip("Amout of sleep time to add when delivery is successful")]
    public float SleepValue = 5f;
    bool isDeliveryCompleted = false;
    public UnityEvent OnDeliveryCompleted;
    public UnityEvent OnCatEntered;
    public UnityEvent OnWrongItem;
    
    
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
                isDeliveryCompleted = true;
            }
            else
            {
                OnWrongItem?.Invoke();
            }
        }
        else if (other.CompareTag("Player") && !isDeliveryCompleted)
        {
            OnCatEntered?.Invoke();
        }
    }
}
