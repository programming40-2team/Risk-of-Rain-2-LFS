using System.Collections;
using UnityEngine;

public class Item1019Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid => 1019;
    private bool isactive=false;

    public void ApplyPassiveEffect()
    {

        GameObject go = null;
        if (!isactive)
        {
            isactive= true;
             go = Managers.Resource.Instantiate("Item1019Skill");
            go.AddComponent<Item1019Component>();
        }
        else
        {
            go.GetComponent<Item1019Component>().SetStats(Managers.ItemInventory.Items[Itemid].Count);
        }
      
    }

  
}
