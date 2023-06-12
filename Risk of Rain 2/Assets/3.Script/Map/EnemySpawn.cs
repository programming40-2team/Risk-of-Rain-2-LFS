using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [Header("소환할 몬스터")]
    [SerializeField] List<GameObject> _monsterPrefab;
    [Header("랜덤스폰 위치")]
    [SerializeField] Transform[] _spawnPoint;
    private bool _EnemySpawned = false;

    private void Start()
    {
        _monsterPrefab.Clear();
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Imp"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Lemurian"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Golem"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Geep"));
    }

    private void OnTriggerEnter(Collider other)
    {
        int random = Random.Range(0, _spawnPoint.Length);
        Transform randomSpawnPoint = _spawnPoint[random];

        int _monsterRandomCount = Random.Range(1, 5);
        if (other.CompareTag("Player") && !_EnemySpawned)
        {
            _EnemySpawned = true;
            Debug.Log("스폰 구역 입장");
            for (int i = 0; i < _monsterRandomCount; i++)
            {
                int _monsterRandomObject = Random.Range(0, _monsterPrefab.Count);
                if (Physics.Raycast(randomSpawnPoint.position + Vector3.up * 5, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                {
                    GameObject _enemy = Managers.Resource.Instantiate($"{_monsterPrefab[_monsterRandomObject].name}");
                    _enemy.GetComponent<NavMeshAgent>().Warp(hit.point);

                }


                //_enemy.SetRandomPositionSphere(2, 7, 2, other.transform);
            }

        }
    }
}
