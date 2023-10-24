using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraFollowTransform;
    public CinemachineTargetGroup group;

    public void SetupCamera() {
        GameObject localPlayer = GameNetworkManager.Instance.localPlayer;
        cameraFollowTransform.position = localPlayer.transform.position;
        cameraFollowTransform.parent = localPlayer.transform;
    }

    public void DisconnectCamera() {
        cameraFollowTransform.position = Vector3.zero;
        cameraFollowTransform.parent = null;
        group.m_Targets[0].radius = 0.1f;
    }

    public void ServerCamera() {
        group.m_Targets[0].radius = 5f;
    }
}
