using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("소환할 몬스터")]
    [SerializeField] GameObject _monsterPrefab;
    [Header("랜덤스폰 위치")]
    [SerializeField] Transform[] _spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        int random = Random.Range(0, _spawnPoint.Length);
        Transform randomSpawnPoint = _spawnPoint[random];

        if (other.CompareTag("Player"))
        {
            Debug.Log("스폰 구역 입장");

            Instantiate(_monsterPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation); 
        }
    }
}
