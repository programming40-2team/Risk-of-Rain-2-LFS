using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] Material _outline;
    private Renderer _renderer;
    public List<Material> materialList = new List<Material>();

    [SerializeField] GameObject _bossPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] ParticleSystem _bossRazer;

    [SerializeField] Animation _HalfSphere;

    private void Awake()
    {
        _renderer = this.GetComponent<Renderer>();

        //SoundManager.instance.PlayBGM("Stage1Bgm");
    }

    private void Start()
    {
        if (_bossRazer.isPlaying)
        {
            _bossRazer.Stop();
            _HalfSphere.Stop();
        }
    }

    private void OnTriggerEnter(Collider other) // 외곽선 적용
    {
        if (other.CompareTag("Player"))
        {
            materialList.Clear();
            materialList.AddRange(_renderer.sharedMaterials);
            materialList.Add(_outline);

            _renderer.materials = materialList.ToArray();
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        Transform bossSpawnPoint = _spawnPoint;

        if (other.CompareTag("Player"))
        {
            Debug.Log("입장");

            //UI이벤트 발생
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("입력 및 보스 생성");

                Instantiate(_bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //제단에서 e키를 누르면 보스가 소환될 지점
                BossRazer();
                _HalfSphere.Play();

                //보스가 생성되면 게임의 현재 상태를 ActiveTelePort로 바꾸어 관련 UI들 갱신!
                Managers.Game.GameState = Define.EGameState.ActiveTelePort;
            }
        }
    }

    private void OnTriggerExit(Collider other) // 외곽선 해제
    {
        if (other.CompareTag("Player"))
        {
            materialList.Clear();
            materialList.AddRange(_renderer.sharedMaterials);
            materialList.Remove(_outline);

            _renderer.materials = materialList.ToArray();

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

}
