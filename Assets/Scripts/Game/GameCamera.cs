using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public string playerTag = "Player";

    Camera _camera;
    static List<Camera> gameCameras = new();

    void Awake() {
        _camera = GetComponentInChildren<Camera>();
        if (gameCameras.Count > 0) _camera.enabled = false;
        gameCameras.Add(_camera);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(playerTag)) {
            foreach (Camera cam in gameCameras) cam.enabled = cam == _camera;
        }
    }
}
