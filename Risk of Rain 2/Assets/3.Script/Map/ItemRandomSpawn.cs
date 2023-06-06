using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] _spawnPrefab;
    [SerializeField] Transform[] _spawnPoint;

    private void Awake()
    {
        ShuffleSpawnPoints();

        int spawnPointNum = _spawnPrefab.Length;

        for (int i = 0; i < spawnPointNum; i++)
        {
            Instantiate(_spawnPrefab[i], _spawnPoint[i].position, _spawnPoint[i].rotation);
        }
    }

    private void ShuffleSpawnPoints()
    {
        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            int random = Random.Range(i, _spawnPoint.Length);

            Transform temp = _spawnPoint[random];
            _spawnPoint[random] = _spawnPoint[i];
            _spawnPoint[i] = temp;
        }
    }
}