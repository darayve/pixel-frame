using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVC;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float initialIntensity;

    private void Awake()
    {
        Instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBMCP =
            cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBMCP.m_AmplitudeGain = intensity;

        initialIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBMCP =
                cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBMCP.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
