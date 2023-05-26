using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public int Itemprice;
    ItemContainerPriceUI itempriceUI;
    public int ContainItemCode;
    private bool isUse = false;

    [SerializeField] private Transform itemmodeltrans;
    private void Start()
    {
        Itemprice = Random.Range(25, 50) + (1 + (int)Managers.Game.Difficulty) * Random.Range(1, 10);
        itempriceUI = Managers.UI.MakeWorldSpaceUI<ItemContainerPriceUI>();
        itempriceUI.itemPrice = Itemprice;
        itempriceUI.transform.SetParent(transform);


        ContainItemCode = Managers.ItemInventory.Items.Keys.ToList()[Random.Range(0, Managers.ItemInventory.Items.Count)];

        //모델과 실제 아이템 프리팹 구분 합시다.
        //모델은 그냥 장식용으로 납두고 실제 아이템에 콜리더를 붙여서 획득 및 인벤토리 추가 하는 방식으로


        if (itemmodeltrans != null)
        {
            GameObject itemmodel = Managers.Resource.Instantiate($"item{ContainItemCode}");
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
        if (other.CompareTag("Player") && !isUse)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this); //상자 UI
            if (Managers.Game.Gold > Itemprice)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //골드 감소 및 중복 구매 방지를 위한 불값 확인
                    Managers.Game.Gold -= Itemprice;
                    isUse = true;
                    itempriceUI.gameObject.SetActive(false);

                    //게임에 존재하는 3개중  원하는 거 집는거..

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
                            itemContainer.isUse = true;
                            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, itemContainer);
                            itemContainer.itempriceUI.gameObject.SetActive(false);
                        }
                    }

                    #endregion

                    //아이템 생성 => 아이템은 아이템 박스로 직접 얻는 것이 아니라 필드에 떨어진 아이템을 줍는 형식
                    GameObject item = Managers.Resource.Instantiate($"Fielditem{ContainItemCode}", gameObject.SetItemSpawnPosition());
                    item.AddComponent<Fielditem>().FieldItemCode = ContainItemCode;
                }
            }
            Debug.Log("상호작용 키를 누르면 아이템을 획득 이 아니라 생성 ");
            Debug.Log("상자로부터 랜덤 위치 생성 인데, 나중에 영일이가 이펙트로 생성된 지점 과 박스 오브젝트 점을 뭐" +
                "포물선으로 연결해서 잘 해주지 않을까? ");
            Debug.Log("아이템은 생성되고, 독자적인 Collider를 가지고 있어서 해당 아이템 Collider에 들어가면 아이템을 획득");
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


    //랜덤 스폰 위치 지정해주는 방식인데 굳이 여긷다 둘필요 없이 확장메서드화 시켰음 GameObject에서 모두 사용이 가능하도록
    //private Vector3 SetItemSpawnPosition()
    //{
    //    Debug.Log("아이템 날라가는 소리 및 이펙트 여기");
    //    Collider collider = GetComponent<Collider>();
    //    Bounds colliderBounds = collider.bounds;
    //    Vector3 colliderSize = colliderBounds.size;
    //    Vector3 colliderCenter = colliderBounds.center;


    //    float angle = Random.Range(0f, Mathf.PI * 2f);

    //    // 랜덤한 거리를 생성
    //    float distance = Random.Range(5f, 10f);

    //    // 삼각함수를 사용하여 위치 계산
    //    float xPos = transform.position.x + Mathf.Cos(angle) * distance;
    //    float zPos = transform.position.z + Mathf.Sin(angle) * distance;
    //    Vector3 CirclePos = new Vector3(xPos, transform.position.y, zPos);

    //    float additionalHeight = 5.0f;

    //    //새롭게 잡힌 포지션은, X,Z 좌표값은 랜덤한 반경, 각도를 통해 얻은 값
    //    //Y 값(높이)는 현재 위치 ( 콜리터 중심점 + 백터 위*(콜리터 가장 윗자리  + 추가적인 아이템이 올라갈높이)
    //    Vector3 newPosition = colliderCenter + CirclePos + Vector3.up * (colliderSize.y * 0.5f + additionalHeight);
    //    return newPosition;
    //}
}
