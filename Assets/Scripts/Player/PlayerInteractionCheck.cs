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
                _interactablesInRange[0].Interact(GrabPoint);
                _currentlyGrabbedObject = _interactablesInRange[0];
                _interactablesInRange.RemoveAt(0);
            }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if(other.TryGetComponent<IInteractable>(out var interactable))
        {
            Debug.Log("Interactable Found");
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            _interactablesInRange.Remove(interactable);
        }
    }
}