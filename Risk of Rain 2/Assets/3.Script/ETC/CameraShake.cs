using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.Mathematics;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private float _shakeTimer;

    private void Awake()
    {
        TryGetComponent(out _virtualCamera);
        _cinemachineBasicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        CheckTimer();
    }

    /// <summary>
    /// 카메라 진동 메서드 
    /// 첫 번째 인수에 강도, 두 번째 인수에 시간입니다.
    /// </summary>
    /// <param name="intensity"></param>
    /// <param name="time"></param>
    /// 
    public void ShakeCamera(float intensity, float time)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _shakeTimer = time;
    }

    private void CheckTimer()
    {
        if (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
