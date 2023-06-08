using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.VFX;
using UnityEditor;

public class StarShip : MonoBehaviour
{
    private Rigidbody _starshipRigidbody;
    private Collider _starshipCollider;
    [SerializeField] private float _starShipSpeed;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private VisualEffect _starShipEffect;
    [SerializeField] private GameObject _groundEffect;

    private void Awake()
    {
        TryGetComponent(out _starshipRigidbody);
        TryGetComponent(out _starshipCollider);
    }

    private void Start()
    {
        _starshipRigidbody.velocity = Vector3.down * _starShipSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            Instantiate(_groundEffect, hit.point, Quaternion.Euler(90, 0, 0));
        }
        _virtualCamera.GetComponent<CameraShake>().ShakeCamera(5, 0.7f);
        StartCoroutine(SlowSmoke_co());
    }

    private IEnumerator SlowSmoke_co()
    {
        float startValue = _starShipEffect.GetFloat("SmokeRate");
        float offset = 0.2f;
        while (_starShipEffect.GetFloat("SmokeRate") >= 0f)
        {
            startValue -= offset;
            _starShipEffect.SetFloat("SmokeRate", startValue);
            yield return null;
        }
        EndStarShip();
    }

    private void EndStarShip()
    {
        _starshipRigidbody.velocity = Vector3.zero;
        _starshipRigidbody.useGravity = false;
        _starshipCollider.enabled = false;
    }

    /// <summary>
    /// 플레이어 카메라로 전환하는 함수입니다. E키를 눌러 플레이어가 나오면 실행시켜 주세요.
    /// </summary>
    private void ChangeCamera()
    {
        _playerCamera.SetActive(true);
        _virtualCamera.enabled = false;
    }

}
