using System.Collections.Generic;
using UnityEngine;

public enum InteractMode
{
    CanMeow,
    CanInteract,
    CanDrop
}
public class PlayerInteractionCheck : MonoBehaviour
{
    public Transform GrabPoint;
    public PlayerController PlayerController;
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    private IInteractable _currentlyGrabbedObject;
    public event System.Action OnInteractFailed;
    public event System.Action<InteractMode> OnInteractUpdated;
    private InteractMode _interactMode;
    
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
                return;
            }
            
            OnInteractFailed?.Invoke();
        };
    }

    private void UpdateInteractMode()
    {
        if (_currentlyGrabbedObject != null)
        {
            _interactMode = InteractMode.CanDrop;
        }
        else if (_interactablesInRange.Count > 0)
        {
            _interactMode = InteractMode.CanInteract;
        }
        else
        {
            _interactMode = InteractMode.CanMeow;
        }
        OnInteractUpdated?.Invoke(_interactMode);
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
        UpdateInteractMode();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            RemoveInteractable(interactable);
        }
        UpdateInteractMode();
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