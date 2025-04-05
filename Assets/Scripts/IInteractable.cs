using System;
using UnityEngine;

public interface IInteractable 
{
    void Interact(Transform parent);
    void Drop();
    event Action OnDestroyed;
}
