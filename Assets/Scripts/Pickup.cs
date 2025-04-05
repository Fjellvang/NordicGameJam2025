using System;
using DG.Tweening;
using UnityEngine;


public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] Quaternion pickedUpRotation;
    [SerializeField] PickupType pickupType;
    [SerializeField] Outline outline;
    private float startOutlineWidth;
    public void SetInteractable(bool isInteractable)
    {
        if (isInteractable)
        {
            DOTween.To(
                () => outline.OutlineWidth, 
                x => outline.OutlineWidth = x, 3, 0.5f).SetEase(Ease.OutBounce);
        }
        else
        {
            DOTween.To(
                () => outline.OutlineWidth, 
                x => outline.OutlineWidth = x, startOutlineWidth, 0.5f)
                .SetEase(Ease.OutBack);
        }
    }
    
    void Awake()
    {
        startOutlineWidth = outline.OutlineWidth;
    }

    public event Action OnDestroyed;
    public PickupType PickupType => pickupType;
    public void Interact(Transform parent)
    {
        transform.SetParent(parent);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
        transform.DOLocalRotate(pickedUpRotation.eulerAngles, 0.5f).SetEase(Ease.OutBack);
    }

    public void Drop()
    {
        var currentParent = transform.parent;
        transform.SetParent(null);
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5 + currentParent.forward * 5, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        OnDestroyed?.Invoke();
    }
}

public enum PickupType
{
    None,
    Hammer,
    Guitar,
}