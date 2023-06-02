using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemInventoryManager
{

    //구조를 어떻게 가져가야할지.. 어려워서 
    //그냥 Dict 자료구조기도 하고 해서 Items 에 추가하고 Passive 면 Passiec 추가 하는 방식으로 가겠습니다.
    public Dictionary<int, Item> Items { get; } = new Dictionary<int, Item>();
    public Dictionary<int, Item.ActiveItem> ActiveItem { get; } = new Dictionary<int, Item.ActiveItem>();
    public Dictionary<int, Item.PassiveItem> PassiveItem { get; } = new Dictionary<int, Item.PassiveItem>();
    public Dictionary<Define.WhenItemActivates, Dictionary<int,Item>> WhenActivePassiveItem { get; } = new Dictionary<Define.WhenItemActivates, Dictionary<int,Item>>();
    public Dictionary<Define.ItemType, Item> MyActiveItem { get; } = new Dictionary<Define.ItemType, Item>();

    public int TempItemCode { get; private set; } = -1;

    public void init()
    {
        //아이템 종류가 많지 않으니, 그냥 인벤토리에 넣고 시작
        //아이템 count의 개수에 따라서 활성 비활성화 처리 예정
        //아이템 구조..이게 맞나?ㅋ 구조를 어떻게 가져가야할 지 모르겠
        foreach (Data.ItemData itemdata in Managers.Data.ItemDataDict.Values)
        {
            if (itemdata.itemType.Equals(Define.ItemType.Active))
            {
                Item item = Item.MakeItem(itemdata);
                ActiveItem.Add(itemdata.itemcode,(Item.ActiveItem)item);
                Items.Add(itemdata.itemcode,item);
                // (Item.ActiveItem)Item.MakeItem(itemdata));
            }
            else
            {
                Item item = Item.MakeItem(itemdata);
                PassiveItem.Add(itemdata.itemcode, (Item.PassiveItem)item);
                Items.Add(itemdata.itemcode, item);
            }
           // Items.Add(itemdata.itemcode, Item.MakeItem(itemdata));
        }

    }


    public bool FindItem(int itemcode)
    {
        if (Items.ContainsKey(itemcode))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool AddItem(int itemcode, Transform trans = null, int count = 1)
    {
        if (Items[itemcode].ItemType.Equals(Define.ItemType.Passive))
        {
          return  AddPassive(itemcode, count);
        }
        else
        {
           return AddActive(itemcode, trans, count);
        }
    }

    private bool AddActive(int itemcode, Transform trans = null, int count = 1)
    {
        //액티브 아이템의 경우 Dict에서 현재 보유중인지 미보유중인지만 확인할 예정
        //보유중일 경우 아이템 코드 바꿔치기, 타 아이템 획득 시, 아이템 교체 및 UI 갱신
        //아이템 사용 은 itemInventory의 MyACtiveITem을 통해서 진행ㅇ할 예정
        if (MyActiveItem.ContainsKey(Define.ItemType.Active))
        {
            TempItemCode = MyActiveItem[Define.ItemType.Active].ItemCode;
            MyActiveItem[Define.ItemType.Active] = Items[itemcode];
            Managers.Event.EquipItemChange?.Invoke(itemcode);
            return false;

        }
        else
        {
            MyActiveItem.Add(Define.ItemType.Active, Items[itemcode]);
            Managers.Event.EquipItemChange?.Invoke(itemcode);
            return true;
        }

      

  
    }
    private bool AddPassive(int itemcode, int count = 1)
    {
        //패시브 아이템 갱신

        //아이템을 획득할 경우 Add함수를 이용해 쉽게 아이템s 을 추가할 수 있습니다.

        if (Items.TryGetValue(itemcode, out Item existingItem))
        {
            Items[itemcode].Count++;
        }
        else
        {
            //지금 버전에서는 아이템 개수가 적어서
            //최적화를 위해 미리 생성해두고 아이템 개수만 증가시키기로 했음
            //따라서 아래 코드는 생성될 일이 없음
            Item item = Item.MakeItem(Managers.Data.ItemDataDict[itemcode]);
            Add(item, count);
        }

       // PassiveItem[itemcode].Count += count;
        switch (PassiveItem[itemcode].WhenItemActive)
        {
            case Define.WhenItemActivates.Always:

                //첫 딕셔너리가 없을 경우 생성 후 넣어주기
                if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.Always))
                {
                    WhenActivePassiveItem.Add(Define.WhenItemActivates.Always, new Dictionary<int, Item>());

                    WhenActivePassiveItem[Define.WhenItemActivates.Always].Add(itemcode, PassiveItem[itemcode]);
                }
                else
                {
                    //딕셔너리 가 존재한다면  + 키가 있는지 확인해서 키가 없으면 새로 만들어서 Dict 에 넣어주기
                    //없다면 Add 로 키 + Value 값을 넣어주기
                    if (!WhenActivePassiveItem[Define.WhenItemActivates.Always].ContainsKey(itemcode))
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.Always].Add(itemcode, PassiveItem[itemcode]);
                    }
                }
                Managers.ItemApply.AddPassiveSkill(itemcode);
                break;
            case Define.WhenItemActivates.AfterBattle:
                if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.AfterBattle))
                {
                    WhenActivePassiveItem.Add(Define.WhenItemActivates.AfterBattle, new Dictionary<int, Item>());
                    WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Add(itemcode, PassiveItem[itemcode]);
                }
                else
                {
                    if (!WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].ContainsKey(itemcode))
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Add(itemcode, PassiveItem[itemcode]);
                    }
                }
                Managers.ItemApply.AddAfterBattleSkill(itemcode);
                break;
            case Define.WhenItemActivates.InBattle:
                if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.InBattle))
                {
                    WhenActivePassiveItem.Add(Define.WhenItemActivates.InBattle, new Dictionary<int, Item>());
                    WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Add(itemcode, PassiveItem[itemcode]);
                }
                else
                {
                    if (!WhenActivePassiveItem[Define.WhenItemActivates.InBattle].ContainsKey(itemcode))
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Add(itemcode, PassiveItem[itemcode]);
                    }

                }
                Managers.ItemApply.AddInBattleSkill(itemcode);
                break;
                 case Define.WhenItemActivates.NotBattle:
                if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.NotBattle))
                {
                    WhenActivePassiveItem.Add(Define.WhenItemActivates.NotBattle, new Dictionary<int, Item>());
                    WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Add(itemcode, PassiveItem[itemcode]);
                }
                else
                {
                    if (!WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].ContainsKey(itemcode))
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Add(itemcode, PassiveItem[itemcode]);
                    }
                }
                break;
        }

        Debug.Log("실험용 추후  Passive 제외 제거 해야함");
        Managers.ItemApply.ExcuteAfterSkills(GameObject.FindGameObjectWithTag("Monster").transform);
        Managers.ItemApply.ExcuteInSkills();
        Managers.ItemApply.ApplyPassiveSkill(itemcode);
        Managers.Event.AddItem?.Invoke(itemcode);
        return true;
    }

    private void Add(Item item, int count = 1)
    {
        Items.Add(item.ItemCode, item);
        if (Items[item.ItemCode].ItemType.Equals(Define.ItemType.Active))
        {
            ActiveItem.Add(item.ItemCode, (Item.ActiveItem)item);

        }
        else
        {
            PassiveItem.Add(item.ItemCode, (Item.PassiveItem)item);
        }
    }
    public int FindCode(Item item)
    {
        foreach (int i in Items.Keys)
        {
            if (Items[i].Equals(item))
            {
                return i;
            }
        }
        return -1;
    }
    //아이템 제거 기능이 필요 없음
    /*  public bool FindItemAndRemove(Item item)
      {
          int code = FindCode(item);
          if (code.Equals(-1)) return false;
          else
          {
              Items[code].Count--;
              if (Items[code].Count.Equals(0))
              {
                  Items.Remove(code);
              }

          }
          return true;
      }
    */

    public Item Get(int itemId)
    {
        Item item = null;
        Items.TryGetValue(itemId, out item);
        return item;
    }
    public Item Find(Func<Item, bool> condition)
    {
        foreach (Item item in Items.Values)
        {
            if (condition.Invoke(item))
                return item;
        }

        return null;
    }

    public void Clear()
    {
        Items.Clear();
    }
}

