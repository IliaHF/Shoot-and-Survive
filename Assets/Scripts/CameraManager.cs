using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraFollowTransform;

    public void SetupCamera() {
        GameObject localPlayer = GameNetworkManager.Instance.localPlayer;
        cameraFollowTransform.position = localPlayer.transform.position;
        cameraFollowTransform.parent = localPlayer.transform;
    }

    public void DisconnectCamera() {
        cameraFollowTransform.position = Vector3.zero;
        cameraFollowTransform.parent = null;
    }
}
