using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private MeshRenderer _objectMesh;
    private Material _mat;
    [SerializeField] Material _highlightMaterial;

    [SerializeField] GameObject _bossPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] ParticleSystem _bossRazer;

    private void Awake()
    {
        //bossRazer = GetComponent<ParticleSystem>();
        _objectMesh = GetComponent<MeshRenderer>();
        _mat = _objectMesh.material;
    }
    private void Start()
    {
        if (_bossRazer.isPlaying)
        {
            _bossRazer.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Transform bossSpawnPoint = _spawnPoint;

        if (other.CompareTag("Player"))
        {
            Debug.Log("입장");
            Highlight();

            //UI이벤트 발생
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("입력");
                
                Instantiate(_bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //제단에서 e키를 누르면 보스가 소환될 지점
                BossRazer();

                //보스가 생성되면 게임의 현재 상태를 ActiveTelePort로 바꾸어 관련 UI들 갱신!
                Managers.Game.GameState = Define.EGameState.ActiveTelePort;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetHighlight();
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
        }
    }

    private void BossRazer()
    {
        if (!_bossRazer.isPlaying)
        {
            _bossRazer.Play();
        }
    }

    private void Highlight()
    {
        _objectMesh.material = _highlightMaterial;
    }

    private void ResetHighlight()
    {
        _objectMesh.material = _mat;
    }
}