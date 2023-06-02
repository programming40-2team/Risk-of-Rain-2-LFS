using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject _spawnPrefab;
    [SerializeField] Transform[] _spawnPoint;

    private void Awake()
    {
        int random = Random.Range(0, _spawnPoint.Length);
        Transform randomSpawnPoint = _spawnPoint[random];

        Instantiate(_spawnPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}