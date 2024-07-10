using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public CinemachineBrain cinemachineBrain;

    public CinemachineFreeLook freeLookCamera;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    public IEnumerator CentreCameraInstant()
    {
        freeLookCamera.m_RecenterToTargetHeading.m_WaitTime = 0f;
        freeLookCamera.m_RecenterToTargetHeading.m_RecenteringTime = 0f;
        freeLookCamera.m_RecenterToTargetHeading.m_enabled = true;

        yield return new WaitForSeconds(0.01f);

        freeLookCamera.m_RecenterToTargetHeading.m_enabled = false;
        freeLookCamera.m_RecenterToTargetHeading.m_WaitTime = 1f;
        freeLookCamera.m_RecenterToTargetHeading.m_RecenteringTime = 2f;
    }
}