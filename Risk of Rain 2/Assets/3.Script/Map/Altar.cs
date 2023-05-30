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

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("입력");
                Instantiate(_bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //제단에서 e키를 누르면 보스가 소환될 지점
                BossRazer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetHighlight();
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