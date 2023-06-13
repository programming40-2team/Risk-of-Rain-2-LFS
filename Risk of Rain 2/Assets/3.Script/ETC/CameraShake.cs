using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour, IListener
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private float _shakeTimer;


    private void Awake()
    {
        TryGetComponent(out _virtualCamera);
        _cinemachineBasicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        Managers.Event.AddListener(Define.EVENT_TYPE.CameraShake, this);
    }

    private void Update()
    {
        CheckTimer();
        if(Input.GetKeyDown(KeyCode.T))
        {
            ShakeCamera(10f, 5f);

        }
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

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        if(Event_Type == Define.EVENT_TYPE.CameraShake)
        {
            Debug.Log("sldkhslfskdhflasfhsd;fsjdhkfs");
            ShakeCamera(10f, 5f);
        }
    }
}
