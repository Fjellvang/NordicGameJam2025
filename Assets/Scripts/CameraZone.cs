using Unity.Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera camera;
    private CameraManager cameraManager;
    
    public CinemachineCamera Camera => camera;

    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraManager>();
        if (camera.LookAt == null)
        {
            camera.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraManager.SetCameraZone(this);
        }
    }
}