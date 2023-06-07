using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public int Itemprice;
    ItemContainerPriceUI itempriceUI;
    public int _containItemCode;
    private bool _Isuse = false;

    [SerializeField] private Transform itemmodeltrans;
    private void Start()
    {
        Itemprice = Random.Range(25, 50) + (1 + (int)Managers.Game.Difficulty) * Random.Range(1, 10);
        itempriceUI = Managers.UI.MakeWorldSpaceUI<ItemContainerPriceUI>();
        itempriceUI.itemPrice = Itemprice;
        itempriceUI.transform.SetParent(transform);


        _containItemCode = Managers.ItemInventory.Items.Keys.ToList()[Random.Range(0, Managers.ItemInventory.Items.Count)];

        //모델과 실제 아이템 프리팹 구분 합시다.
        //모델은 그냥 장식용으로 납두고 실제 아이템에 콜리더를 붙여서 획득 및 인벤토리 추가 하는 방식으로


        if (itemmodeltrans != null)
        {
            GameObject itemmodel = Managers.Resource.Instantiate($"item{_containItemCode}");
            itemmodel.transform.parent = itemmodeltrans.transform;
            itemmodel.transform.localPosition = Vector3.up * 0.5f;
            itemmodel.AddComponent<UIItemController>();
        }



    }

    //아이템 컨테이너
    // 플레이어를 감지 -> 만약 골드가 높다면 E 키를 누르면
    // 아이템 생성 -> 콜라이더 제거 -> 이벤트 발송
    // 만약 구매하지 않고 Triger Exit 해도 바송
    //근데 Trigger Vs COllsion

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_Isuse)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this); //상자 UI
            if (Managers.Game.Gold > Itemprice)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    #region 3개짜리 컨테이너 일단 1개만 획득 가능하게 구현
                    if (transform.parent == null)
                    {
                        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
                    }
                    else
                    {
                        ItemContainer[] itemContainers = transform.parent.GetComponentsInChildren<ItemContainer>();
                        foreach (ItemContainer itemContainer in itemContainers)
                        {
                            itemContainer._Isuse = true;
                            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, itemContainer);
                            itemContainer.itempriceUI.gameObject.SetActive(false);
                        }
                    }
                    #endregion
                    //골드 감소 및 중복 구매 방지를 위한 불값 확인
                    Managers.Game.Gold -= Itemprice;
                    _Isuse = true;
                    itempriceUI.gameObject.SetActive(false);
                    //아이템 생성 => 아이템은 아이템 박스로 직접 얻는 것이 아니라 필드에 떨어진 아이템을 줍는 형식
                    GameObject _item = Managers.Resource.Instantiate($"Fielditem{_containItemCode}");

                    GameObject _ItemEffect = Managers.Resource.Instantiate("ItemOutEffect");
                    _ItemEffect.transform.position = gameObject.transform.position;
                    _ItemEffect.GetComponent<ItemOutEffect>()._targetPosition = _item.SetRandomPositionSphere(3, 8, 2, gameObject.transform);

                    if (TryGetComponent(out Animator _animoator))
                    {
                        _animoator.SetTrigger("Open");
                    }
                    Debug.Log("아이템 날라가는 소리 및 이펙트 여기1 ");
                    _item.AddComponent<Fielditem>().FieldItemCode = _containItemCode;
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //StartCoroutine(nameof(CollisionExitEvent_co));
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
    }

    IEnumerator CollisionExitEvent_co()
    {
        yield return new WaitForSeconds(0.5f);
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
    }
}