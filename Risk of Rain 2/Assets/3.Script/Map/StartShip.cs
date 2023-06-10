using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShip : MonoBehaviour
{
    [Header("시작할 플레이어")]
    [SerializeField] GameObject _PlayerPrefab;
    [Header("스폰 위치")]
    [SerializeField] Transform _spawnPoint;


    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerCamera;
    private Animation _doorOpen;
    private bool isStart = false;

    [SerializeField] private StarShip _starShip;
    void Awake()
    {
        _doorOpen = GetComponent<Animation>();

        _player.SetActive(true);
        _playerCamera.SetActive(false);
    }

    private void Start()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);
    }
    void Update()
    {
        if (isStart == false && Input.GetKeyDown(KeyCode.E) && _starShip._isGround)
        {
            _doorOpen.Play();
            _player.transform.position = _spawnPoint.transform.position;
            _player.SetActive(true);
            _playerCamera.SetActive(true);
            gameObject.SetActive(false);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);

            //Instantiate(_PlayerPrefab, _spawnPoint.position, _spawnPoint.rotation);
            isStart = true;
        }
    }
}
