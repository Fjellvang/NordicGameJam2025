using UnityEngine;


public interface IInteractable 
{
    void Interact(Transform parent);
    void Drop();
}
    
public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] Quaternion pickedUpRotation;
    private bool isPickedUp;
    public void Interact(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = pickedUpRotation;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isPickedUp = true;
    }
    
    void Update()
    {
        if (isPickedUp)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = pickedUpRotation;
        }
    }

    public void Drop()
    {
        var currentParent = transform.parent;
        transform.SetParent(null);
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5 + currentParent.forward * 5, ForceMode.Impulse);
    }
}