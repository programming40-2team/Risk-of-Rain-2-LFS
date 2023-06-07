using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] _spawnPrefab;
    [SerializeField] Transform[] _spawnPoint;

    private void Start()
    {
        ShuffleSpawnPoints();

        int spawnPointNum = _spawnPrefab.Length;

        for (int i = 0; i < spawnPointNum; i++)
        {
          GameObject go= Managers.Resource.Instantiate(_spawnPrefab[i].name, _spawnPoint[i].position, _spawnPoint[i]);
            go.transform.parent = _spawnPoint[i];
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