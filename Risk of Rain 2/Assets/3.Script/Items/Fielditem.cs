using UnityEngine;

public class Fielditem : MonoBehaviour
{
    public int FieldItemCode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("æ∆¿Ã≈€ »πµÊ º“∏Æ ø©±‚¥Ÿ!");

            if(!Managers.ItemInventory.AddItem(FieldItemCode, gameObject.transform))
            {
                GameObject item = Managers.Resource.Instantiate($"Fielditem{Managers.ItemInventory.TempItemCode}", gameObject.SetItemSpawnPosition());
                item.AddComponent<Fielditem>().FieldItemCode = Managers.ItemInventory.TempItemCode;
            }
            Managers.Resource.Destroy(gameObject);
        }
    }


}
