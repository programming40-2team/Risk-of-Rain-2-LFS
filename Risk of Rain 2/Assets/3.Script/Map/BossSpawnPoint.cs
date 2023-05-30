using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject _teleporterPrefab;
    [SerializeField] Transform[] _spawnPoint;

    private void Awake()
    {
        int random = Random.Range(0, _spawnPoint.Length);
        Transform randomSpawnPoint = _spawnPoint[random];

        Instantiate(_teleporterPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}