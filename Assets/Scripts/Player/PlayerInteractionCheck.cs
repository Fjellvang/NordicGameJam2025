using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionCheck : MonoBehaviour
{
    public Transform GrabPoint;
    public PlayerController PlayerController;
    
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    private IInteractable _currentlyGrabbedObject;

    private void Awake()
    {
        PlayerController.OnInteractPerformed += () =>
        {
            if (_currentlyGrabbedObject != null)
            {
                _currentlyGrabbedObject.Drop();
                _currentlyGrabbedObject = null;
                return;
            }
            
            if (_interactablesInRange.Count > 0)
            {
                var interactable = _interactablesInRange[0];
                interactable.Interact(GrabPoint);
                _currentlyGrabbedObject = interactable;
                RemoveInteractable(interactable);
            }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IInteractable>(out var interactable))
        {
            _interactablesInRange.Add(interactable);
            interactable.SetInteractable(true);
            interactable.OnDestroyed += () =>
            {
                RemoveInteractable(interactable);
            };
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            RemoveInteractable(interactable);
        }
    }
    
    private void RemoveInteractable(IInteractable interactable)
    {
        if (_interactablesInRange.Contains(interactable))
        {
            interactable.SetInteractable(false);
            _interactablesInRange.Remove(interactable);
        }
    }
}