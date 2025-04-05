using System;
using UnityEngine;

public interface IInteractable 
{
    void Interact(Transform parent);
    void Drop();
    void SetInteractable(bool isInteractable);
    event Action OnDestroyed;
}
