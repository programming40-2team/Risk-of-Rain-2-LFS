using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private BeetleQueen _beetleQueen;

    public void Spawn() // TODO : 플레이어가 텔레포트 작동 시켰을때 Spawn해야함
    {
        BeetleQueen beetleQueen = Instantiate(_beetleQueen, transform.position, transform.rotation);

        beetleQueen.OnDeath += () => Destroy(_beetleQueen.gameObject, 10f);
    }
}
