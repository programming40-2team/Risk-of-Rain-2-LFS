public class Item1009Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1009; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.MaxJumpCount = 1 + Managers.ItemInventory.Items[Itemid].Count;

    }


}

