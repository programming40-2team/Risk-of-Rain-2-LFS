using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemySpawn : MonoBehaviour
{
    [Header("소환할 몬스터")]
    [SerializeField] List<GameObject> _monsterPrefab;
    [Header("랜덤스폰 위치")]
    [SerializeField] Transform[] _spawnPoint;
    [Header("스폰 타임")]
    [SerializeField] float _spawnTime;

    private bool _isSpawned = false;

    private void Start()
    {
        _monsterPrefab.Clear();
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Imp"));
         _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Lemurian"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Golem"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Geep"));
        _spawnTime = 15f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isSpawned)
            {
                _isSpawned = true;
                StartCoroutine(BossEnemy_co());
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && (Managers.Game.GameState == Define.EGameState.KillBoss || Managers.Game.GameState == Define.EGameState.ActiveTelePort))
        {
            Managers.Game.ProgressBoss += Time.deltaTime;
            Managers.Event.BossProgress?.Invoke();
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(BossEnemy_co());
            _isSpawned = false;
        }
    }

    private IEnumerator BossEnemy_co()
    {
        while (_isSpawned && true)
        {
            int randomSpawnPoint = Random.Range(0, _spawnPoint.Length);
            int randomMonster = Random.Range(0, _monsterPrefab.Count);

            GameObject randomMonsterPrefab = _monsterPrefab[randomMonster];
            Transform randomPoint = _spawnPoint[randomSpawnPoint];

            if (Physics.Raycast(randomPoint.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, 1 << (int)Define.LayerMask.Enviroment))
            {

                GameObject _enemy = Managers.Resource.Instantiate($"{_monsterPrefab[randomMonster].name}");
                _enemy.GetComponent<NavMeshAgent>().Warp(hit.point);

            }
            yield return new WaitForSeconds(_spawnTime);
        }

    }

}
