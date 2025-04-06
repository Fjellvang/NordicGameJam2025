using System.Collections.Generic;
using DotsShooter;
using UnityEngine;
using UnityEngine.Events;

public class GameStateTracker : Singleton<GameStateTracker>
{
    public PickupType[] PickupTypes;
    private Dictionary<PickupType, bool> _completedPickups;
    public UnityEvent OnAllCompleted;
    
    public override void Awake()
    {
        base.Awake();
        _completedPickups = new Dictionary<PickupType, bool>();
        foreach (var pickupType in PickupTypes)
        {
            _completedPickups[pickupType] = false;
        }
    }
    
    public void SetPickupCompleted(PickupType pickupType)
    {
        if (_completedPickups.ContainsKey(pickupType))
        {
            _completedPickups[pickupType] = true;
        }
        
        if (IsAllCompleted())
        {
            OnAllCompleted?.Invoke();
        }
    }
    
    public bool IsAllCompleted()
    {
        foreach (var completed in _completedPickups.Values)
        {
            if (!completed)
            {
                return false;
            }
        }
        return true;
    }
}