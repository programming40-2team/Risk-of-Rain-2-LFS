public class Item1012Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1012; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.Armor = _playerStatus._survivorsData.Armor * 1.14f * Managers.ItemInventory.Items[Itemid].Count;
    }


}
