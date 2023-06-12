using UnityEngine;

public class Fielditem : MonoBehaviour
{
    public int FieldItemCode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("아이템 획득 소리 여기다!");

            if (!Managers.ItemInventory.AddItem(FieldItemCode, gameObject.transform))
            {
                GameObject item = Managers.Resource.Instantiate($"Fielditem{Managers.ItemInventory.TempItemCode}");
                item.SetRandomPositionSphere();
                Debug.Log("아이템 날라가는 소리 및 이펙트 여기2");
                item.AddComponent<Fielditem>().FieldItemCode = Managers.ItemInventory.TempItemCode;
            }
            Managers.Resource.Destroy(gameObject);
        }
    }


}
