using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryManager
{

    //구조를 어떻게 가져가야할지.. 어려워서 
    //그냥 Dict 자료구조기도 하고 해서 Items 에 추가하고 Passive 면 Passiec 추가 하는 방식으로 가겠습니다.
    public Dictionary<int, Item> Items { get; } = new Dictionary<int, Item>();
    public Dictionary<int, Item> ActiveItem { get; } = new Dictionary<int, Item>();
    public Dictionary<int, Item.PassiveItem> PassiveItem { get; } = new Dictionary<int, Item.PassiveItem>();
    public Dictionary<Define.WhenItemActivates, List<Item>> WhenActivePassiveItem { get; } = new Dictionary<Define.WhenItemActivates, List<Item>>();
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
                ActiveItem.Add(itemdata.itemcode, Item.MakeItem(itemdata));
            }
            else
            {
                PassiveItem.Add(itemdata.itemcode, (Item.PassiveItem)Item.MakeItem(itemdata));
            }
            Items.Add(itemdata.itemcode, Item.MakeItem(itemdata));
        }
        foreach (Item.PassiveItem item in PassiveItem.Values)
        {
            switch (item.WhenItemActive)
            {
                case Define.WhenItemActivates.Always:
                    if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.Always))
                    {
                        WhenActivePassiveItem.Add(Define.WhenItemActivates.Always, new List<Item>());
                        WhenActivePassiveItem[Define.WhenItemActivates.Always].Add(item);
                    }
                    else
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.Always].Add(item);
                    }
                    break;
                case Define.WhenItemActivates.AfterBattle:
                    if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.AfterBattle))
                    {
                        WhenActivePassiveItem.Add(Define.WhenItemActivates.AfterBattle, new List<Item>());
                        WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Add(item);
                    }
                    else
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Add(item);
                    }
                    break;
                case Define.WhenItemActivates.InBattle:
                    if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.InBattle))
                    {
                        WhenActivePassiveItem.Add(Define.WhenItemActivates.InBattle, new List<Item>());
                        WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Add(item);
                    }
                    else
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Add(item);
                    }
                    break;
                case Define.WhenItemActivates.NotBattle:
                    if (!WhenActivePassiveItem.ContainsKey(Define.WhenItemActivates.NotBattle))
                    {
                        WhenActivePassiveItem.Add(Define.WhenItemActivates.NotBattle, new List<Item>());
                        WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Add(item);
                    }
                    else
                    {
                        WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Add(item);
                    }
                    break;
            }
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

        PassiveItem[itemcode].Count += count;
        switch (PassiveItem[itemcode].WhenItemActive)
        {
            case Define.WhenItemActivates.Always:
                WhenActivePassiveItem[Define.WhenItemActivates.Always].Find(s => s.ItemCode.Equals(itemcode)).Count += count;
                break;
            case Define.WhenItemActivates.AfterBattle:
                WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Find(s => s.ItemCode.Equals(itemcode)).Count += count;
                break;
            case Define.WhenItemActivates.InBattle:
                WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Find(s => s.ItemCode.Equals(itemcode)).Count += count;
                break;
            case Define.WhenItemActivates.NotBattle:
                WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Find(s => s.ItemCode.Equals(itemcode)).Count += count;
                break;
        }

        Managers.Event.AddItem?.Invoke(itemcode);
        return true;
    }

    private void Add(Item item, int count = 1)
    {
        Items.Add(item.ItemCode, item);
        if (Items[item.ItemCode].ItemType.Equals(Define.ItemType.Active))
        {
            ActiveItem.Add(item.ItemCode, item);

        }
        else
        {
            PassiveItem.Add(item.ItemCode, (Item.PassiveItem)item);
            switch (PassiveItem[item.ItemCode].WhenItemActive)
            {
                case Define.WhenItemActivates.Always:
                    WhenActivePassiveItem[Define.WhenItemActivates.Always].Find(s => s.ItemCode.Equals(item.ItemCode)).Count += count;
                    break;
                case Define.WhenItemActivates.AfterBattle:
                    WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Find(s => s.ItemCode.Equals(item.ItemCode)).Count += count;
                    break;
                case Define.WhenItemActivates.InBattle:
                    WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Find(s => s.ItemCode.Equals(item.ItemCode)).Count += count;
                    break;
                case Define.WhenItemActivates.NotBattle:
                    WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Find(s => s.ItemCode.Equals(item.ItemCode)).Count += count;
                    break;
            }
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

