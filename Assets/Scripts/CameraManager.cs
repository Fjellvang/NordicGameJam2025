using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraZone defaultCameraZone;
    private CameraZone currentCameraZone;
    private void Awake()
    {
        defaultCameraZone.Camera.Priority = 10;
        currentCameraZone = defaultCameraZone;
    }

    public void SetCameraZone(CameraZone cameraZone)
    {
        currentCameraZone.Camera.Priority = 0;
        currentCameraZone = cameraZone;
        currentCameraZone.Camera.Priority = 10;
    }
}