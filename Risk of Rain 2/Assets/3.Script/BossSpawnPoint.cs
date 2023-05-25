using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject teleporterPrefab;
    [SerializeField] Transform[] spawnPoint;

    private void Awake()
    {
        int random = Random.Range(0, spawnPoint.Length);
        Transform randomSpawnPoint = spawnPoint[random];

        Instantiate(teleporterPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}
