using System;
using System.Collections.Generic;


public class ItemInventoryManager
{
    public Dictionary<int, Item> Items { get; } = new Dictionary<int, Item>();
    public Dictionary<int, Item> ActiveItem { get; } = new Dictionary<int, Item>();
    public Dictionary<int, Item.PassiveItem> PassiveItem { get; }=new Dictionary<int, Item.PassiveItem>();
    public Dictionary<Define.WhenItemActivates, List<Item>> WhenActivePassiveItem { get; } = new Dictionary<Define.WhenItemActivates, List<Item>>();
    
    public void init()
    {
        //아이템 종류가 많지 않으니, 그냥 인벤토리에 넣고 시작
        //아이템 count의 개수에 따라서 활성 비활성화 처리 예정
        //아이템 구조..이게 맞나?ㅋ 구조를 어떻게 가져가야할 지 모르겠
        foreach (Data.ItemData itemdata in Managers.Data.ItemDataDict.Values)
        {
            if(itemdata.itemType.Equals(Define.ItemType.Active))
            {
                ActiveItem.Add(itemdata.itemcode, Item.MakeItem(itemdata));
            }
            else
            {
                PassiveItem.Add(itemdata.itemcode, (Item.PassiveItem)Item.MakeItem(itemdata));
            }
            Items.Add(itemdata.itemcode, Item.MakeItem(itemdata));
        }
        foreach(Item.PassiveItem item in PassiveItem.Values)
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
                        WhenActivePassiveItem[Define.WhenItemActivates.Always].Add(item);
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
    public bool Add(int itemcode, int count = 1)
    {
        //아이템을 획득할 경우 Add함수를 이용해 쉽게 아이템s 을 추가할 수 있습니다.
      
        if (Items.TryGetValue(itemcode, out Item existingItem))
        {
            existingItem.Count += count;
        }
        else
        {
            Item item = Item.MakeItem(Managers.Data.ItemDataDict[itemcode]);
            Add(item, count);
        }
        //저장소 갱신
        if (Items[itemcode].ItemType.Equals(Define.ItemType.Active))
        {
            ActiveItem[itemcode].Count += count;

        }
        else
        {
            PassiveItem[itemcode].Count += count;
            switch (PassiveItem[itemcode].WhenItemActive)
            {
                case Define.WhenItemActivates.Always:
                    WhenActivePassiveItem[Define.WhenItemActivates.Always].Find(s => s.ItemCode.Equals(itemcode)).Count+=count;
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
        }




        Managers.Event.AddItem?.Invoke(itemcode);
        return true;
    }

    private void Add(Item item, int count = 1)
    {
        if (Items.ContainsKey(item.ItemCode))
        {
            Items[item.ItemCode].Count += count;

        }
        else
        {
            Items.Add(item.ItemCode, item);

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

