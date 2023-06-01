using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveActiveItem : ItemPrimitiive
{
    private int _myEquipItem = -1;
    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();
        Managers.Event.EquipItemChange -= SetMyItemCode;
        Managers.Event.EquipItemChange += SetMyItemCode;

        Managers.Event.ExcuteActiveItem -= OnActiveSkill;
        Managers.Event.ExcuteActiveItem += OnActiveSkill;
    }
    void Start()
    {
        Init();
    }
    private void SetMyItemCode(int itemcode)
    {
        _myEquipItem = itemcode;
        FindObjectOfType<CommandoSkill>().SkillQColldown = Managers.ItemInventory.ActiveItem[itemcode].Cooltime;
    }
    private void OnActiveSkill()
    {
        if (_myEquipItem.Equals(-1))
        {
            Debug.Log($"장착 중인 스킬 없음 : 아이템 코드 {_myEquipItem} ");
            return;
        }
        switch (_myEquipItem)
        {
            case 1020:
                StopCoroutine(nameof(Item1020_co));
                StartCoroutine(nameof(Item1020_co));
                break;
            case 1021:
                _playerStatus.OnHeal(_playerStatus.MaxHealth*0.5f);
                break;
            case 1022:
                GameObject Item1022 = Managers.Resource.Instantiate("Item1022Skill");
                Item1022.GetOrAddComponent<Item1022Skill>();
                break;
            case 1023:
                StopCoroutine(nameof(Item2023_co));
                StartCoroutine(nameof(Item2023_co));
                break;
            case 1024:
                StopCoroutine(nameof(Item2024_co));
                StartCoroutine(nameof(Item2024_co));
                break;
            case 1025:
                Item1025SKilll();
                break;

        }
    }

    private IEnumerator Item2023_co()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject item1023 = Managers.Resource.Instantiate("Item1023Skill");
            item1023.SetRandomPositionSphere(Random.Range(2, 5), Random.Range(2, 6), Random.Range(2, 4), Player.transform);
            yield return new WaitForSeconds(0.5f);
        }
      
    }
    private void OnDisable()
    {
        Managers.Event.EquipItemChange -= SetMyItemCode;
        Managers.Event.ExcuteActiveItem -= OnActiveSkill;
    }
    private IEnumerator Item2024_co()
    {
        float chacne = _playerStatus.ChanceBlockDamage;
        _playerStatus.ChanceBlockDamage = 100;
        yield return new WaitForSeconds(10.0f);
        _playerStatus.ChanceBlockDamage = chacne;
    }
    private IEnumerator Item1020_co()
    {
        bool item1007Spawned = false;
        if (!item1007Spawned)
        {
            item1007Spawned = true;
            for (int i = 0; i < 12; i++)
            {
                GameObject item1007 = Managers.Resource.Instantiate("Item1007Skill");
                item1007.transform.position = Player.transform.position;

                item1007.SetRandomPositionSphere(2, 5, 5);
                Debug.Log("위치 2개의 직선 을 이어주는 연기 필요");
                
                Debug.Log("연동방법   함수 (item1007.transform.position ,item1007.SetRandomPositionSphere(1, 1, 5) 위의 item1007.SEtRandomPositionSphere 지워야함");
                item1007.GetOrAddComponent<Item1007SkillComponent>();

            }
            yield return new WaitForSeconds(3.0f);
            item1007Spawned = false;
        }

    }
    private void Item1025SKilll()
    {
      

       GameObject Tele1 = Managers.Resource.Instantiate("Item1025Skill");
       GameObject Tele2 = Managers.Resource.Instantiate("Item1025Skill");

        Tele1.GetOrAddComponent<Item1025Skill>().SetTeleCode(1);
        Tele2.GetOrAddComponent<Item1025Skill>().SetTeleCode(2);

        Tele1.SetRandomPositionSphere(2, 4, 0,Player.transform);
        Tele2.SetRandomPositionSphere(200, 400, 0,Player.transform);

    }
}
