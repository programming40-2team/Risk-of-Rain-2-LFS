using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTest : MonoBehaviour
{
    public InputField ItemKeyInput;
    private int Itemcode ;

    private void Start()
    {
        int.TryParse(ItemKeyInput.text, out Itemcode);

    }
    public void SetCode()
    {
        int.TryParse(ItemKeyInput.text, out Itemcode);
    }
    public void ExcuteItem()
    {
        Managers.ItemInventory.AddItem(Itemcode);
    }
    public void TakeDamage()
    {
        if (GameObject.FindGameObjectWithTag("Player").TryGetComponent(out Entity entity))
        {
            entity.OnDamage(20);
        }

    }

}
