using Unity.Cinemachine;
using UnityEngine;

[DefaultExecutionOrder(20)]
public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    CinemachineCamera cinemachineCamera;

    void Start() {
        cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        if (cinemachineCamera.Follow == null) cinemachineCamera.Follow = Player.I.transform;
    }
}
