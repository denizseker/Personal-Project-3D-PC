using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance;

    public GameObject cameraRoot;

    [HideInInspector] public CameraZoom cameraZoom;
    [HideInInspector] public CameraRotation cameraRotation;
    [HideInInspector] public CameraMotion cameraMotion;

    private void Awake()
    {
        Instance = this;

        Instance.cameraZoom = cameraRoot.GetComponent<CameraZoom>();
        Instance.cameraRotation = cameraRoot.GetComponent<CameraRotation>();
        Instance.cameraMotion = cameraRoot.GetComponent<CameraMotion>();
    }

    public void MoveToObject(GameObject _target)
    {
        Instance.cameraMotion.MoveToObject(_target);
        ToggleOnOffCameraInput();
    }

    public void MoveToPlayer()
    {
        Instance.cameraMotion.MoveToObject(Instance.cameraMotion.player);
    }

    public void ToggleOnOffCameraInput()
    {
        //Instance.cameraZoom.isCameraLockedToTarget = !Instance.cameraZoom.isCameraLockedToTarget;
        //Instance.cameraRotation.isCameraLockedToTarget = !Instance.cameraRotation.isCameraLockedToTarget;
        Instance.cameraMotion.isCameraLockedToTarget = !Instance.cameraMotion.isCameraLockedToTarget;
    }

}
