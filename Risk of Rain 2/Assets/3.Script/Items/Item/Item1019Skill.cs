using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//여왕의 분배샘 -> 딱정벌레 소환, 공격 및 체력 보너스 준다.
public class Item1019Skill : ItemPrimitiive
{

    public float radius = 5f; // 반경
    public float rotationSpeed = 50f; // 속도

    private Vector3 rotationAxis;

    public override void Init()
    {
        base.Init();

        // 중심점과 물체의 초기 거리 및 축(axis) 설정
        rotationAxis = (transform.position - Player.transform.position).normalized;
        _playerStatus.Damage = _playerStatus._survivorsData.Damage * 3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count);
        _playerStatus.AddMaxHealth(3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count));
    }
    private void Start()
    {
        Init();

        Debug.Log("가끔씩 나타나는 랜더러 Courtine");
    }

    private void Update()
    {
        rotationAxis = (transform.position - Player.transform.position).normalized;


        // 중심점을 중심으로 물체를 회전시킴
        transform.RotateAround(Player.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
      
        // 회전 후 물체의 위치를 반경에 맞게 조정
        transform.position = Player.transform.position + (transform.position - Player.transform.position).normalized * radius;


    }
    private void OnDisable()
    {
        _playerStatus.Damage = _playerStatus._survivorsData.Damage ;
        _playerStatus.AddMaxHealth( -3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count));
    }
    public void SetStats(int Count)
    {
        _playerStatus.Damage = _playerStatus._survivorsData.Damage * 3 * Count;
        _playerStatus.AddMaxHealth(3 * Count);

    }
}
