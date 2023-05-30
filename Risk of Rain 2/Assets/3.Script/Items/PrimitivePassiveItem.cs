using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PrimitivePassiveItem : ItemPrimitiive
{

    /*
     1. 상시 아이템은 Invoke() 받으면 어떻게 처리할 것인가?
          ==> 스텟 올려주는 것은 그냥 코루틴 실행하면 되는데 생성하는 것은?
     */




    //보너스 점프 카운트...
    PlayerStatus currStartus;
    PlayerMovement playerMoveMent;
    private void Start()
    {
      currStartus=  Player.GetComponent<PlayerStatus>();
        playerMoveMent = Player.GetComponent<PlayerMovement>();
    }
    //기본적으론 플레이어 상태에 따라 한번씩 전부 실행시켜주고, 만약 아이템을 획득한 경우에만 추가적으로 한번더 실행
    //원본 데이터 가지고 있어야 하고, 아이템을 획득한 경우 아이템 타입이 현재 플레이어 상태가 어떠하냐에 따라서
    //갱신 을 시켜줄지를 결정 워차피 상태가 바뀜에 따라 모든 패시브 스킬들을 순회할 예정이니까

    private void ExcuteSkill()
    {
        foreach (var itemkey in Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always].Keys)
        {

        }
       
    }
    private void StopSkill()
    {

    }

   
    //패시브라고 해서 전부 패시브가 아님, 패시브인데 액티브형인 것 같은 스킬이 존재 머리 아픔
    //일단.. 체계를 못잡아서 하드코딩 식으로 가는중 나중에 스킬 더 많아 지면 좀 더 세분화 해서 관리?.. 어렵네\
    //적을 죽여서 발동 시키는 경우 적의 위치도 고려 요소 중 하나 ->  구현 해보면서 생각해보자  단순하게 오보로드 사용?
    private void OnPassiveSkill(int itemcode,Vector3 spawnPos)
    {
        switch(itemcode)
        {
            case 1001:
                //공속 15% 향상 -> 일단 임시로 최대체력 올려놨고 필요한거 따라서 설정할 예정
                currStartus.MaxHealth = currStartus._survivorsData.MaxHealth + 15 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;  
            case 1002:
                //얘는 피해차단이라서 따로 컴포넌트 주던가 해야할듯? -> 이 아니라 그냥 플레이어 Hit 부분에 나만의 작은 함수 하나 넣지 뭐
                break;
            case 1003:
                StopCoroutine(nameof(Item1003_co));
                StartCoroutine(nameof(Item1003_co));
                //플레이어 위치 기준으로 랜덤한 곳 에 메디킷 같은거 하나 생성해주고 거기에 기능을 구현  (완)
                break;
            case 1004:
                
                //치명타 추후 구현 예쩡입니다.
                break;
            case 1005:
                // currStartus.MoveSpeed=currStartus._survivorsData.MoveSpeed*1.14* Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always][itemcode].Count;
                playerMoveMent._bonusMoveSpeed=1.14f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1006:
                //실드 획득인데 실드 관련 속성이 없음
                //일단  체력 생성으로 넣어놨는데 체력 생성 
                currStartus.Health+= 15 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1007:
                StopCoroutine(nameof(Item1007_co));
                StartCoroutine(nameof(Item1007_co));
                //공격 발사 시 미사일 발사 -> 우선 플레이어 앞방향으로 직선으로 나가게 설정
                break;
            case 1008:
                //용암 기능 생성하는 스크립트 생성 적을 처치할 경우 12m 반경에 용암기둥이 생성 되어 350% 피해
                //일단 피해를 입히는 코드는 작성해두었는데 생성 위치가 적 죽은 위치라서 위치 값을 어떻게 줘야 할지?  아니면 따로 이벤트로 발동시켜야 할지 모르겠음
                GameObject item1008= Managers.Resource.Instantiate("Item1008Skill");
                item1008.GetOrAddComponent<Item1008Skill>();
                item1008.transform.position = spawnPos;
                break;
            case 1009:
                playerMoveMent._bonusJumpCount += Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1010:
                currStartus.Health += 1 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                //피해를 입히면 나의 체력이 1 회복... 얘는 몬스터 상태도 봐야 할듯 한데?.. --> 컴포넌트 가져와서 프로퍼티 등으로 확인하면 될듯?..
                break;
            case 1011:
                playerMoveMent._bonusMoveSpeed = 1.3f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1012:
                //치명타 확률 5%증가 치명타 터지면 체력 8  +4 *count 치유
                break;
            case 1013:
                //적을 처치하면 장비 쿨타임 4 + 2 * count초 감소
                break;
            case 1014:
                //일단 1014ㅇ ㅔ쓰는 단검 아이템 model이랑 실제로 나갈 모델이랑 똑같아서 addcomponent 쓰면 좋은데..
                // 다른 아이테 코드랑 헷갈리니 통일성을 위해서 새롭게 prefab만들었음

                StopCoroutine(nameof(Item1014_co));
                StartCoroutine(nameof(Item1014_co));
                //적을 처치하면 단검 3개 생성 -> 단검은 적을 쫓아 가서 적에게 나의 공격 만큼타격을 입힘 +  단검의 개수는 일정 ,
                break;
            case 1015:
                Managers.Resource.Instantiate("Item1015SKill");
                //적을 처치하면 나에게 얼움 폭풍이 생기고 그 안에 있는 몬스터  이속 80% 감소 
                //매번 생성하는 것이 아니라 한번 생성하면 더이상 생성하지 않아도 되며, 플레이어 위치만 계속 따라다니면 됨 
                break;
            case 1016:
                bool Isitem1016Created = false;
                if (!Isitem1016Created)
                {
                  GameObject Item1016 =Managers.Resource.Instantiate("Item1016Skill");
                    Isitem1016Created = true;
                }
                //점프 높이가 증가하여 착지할 때 5~100m 반경의 운동에너지 폭발 (데미지주는 장판 생성) 점프 끝날떄
                //=>  점프 시작할 떄 데미지 주는 것으로 변경
                break;
            case 1017:
                //적에게 공격이 명중하면 유도갈고리 생성

                break;
            case 1018:
                currStartus.MaxHealth = currStartus._survivorsData.MaxHealth;
                currStartus.MaxHealth +=currStartus._survivorsData.MaxHealth+ 40*Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                currStartus.HealthRegen += currStartus._survivorsData.HealthRegen + 1.6f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1019:
                StopCoroutine(nameof(Item1019_co));
                StartCoroutine(nameof(Item1019_co));
                //딱정벌레 생성 -> 딱정벌레가 플레이어 체력 증가
                //30초마다, 딱정벌레 근위병을 소환하여 공격력에 300%,체력에 100% 보너스를 줍니다.
                break;
        }
    }

    private IEnumerator Item1003_co()
    {
        bool item1003Spawned = false;
        if (!item1003Spawned)
        {
            item1003Spawned = true;
            for (int i = 0; i < 3; i++)
            {
                GameObject item1003 = Managers.Resource.Instantiate("Item1003Skill");
                item1003.transform.position = Player.transform.position;
                item1003.GetOrAddComponent<Item1003Skill>();
                item1003.SetRandomPositionSphere();
            }
            yield return new WaitForSeconds(1.5f);
            item1003Spawned = false;
        }
    }
    private IEnumerator Item1014_co()
    {
        if (Util.Probability(30))
        {
            bool item1014Spawned = false;
            if (!item1014Spawned)
            {
                item1014Spawned = true;
                for (int i = 0; i < 3; i++)
                {
                    GameObject item1014 = Managers.Resource.Instantiate("Item1014Skill");
                    item1014.transform.position = Player.transform.position;
                    //ㅑㅅ드
                    item1014.SetRandomPositionSphere(1, 2, 1);

                    item1014.GetOrAddComponent<item1014Skill>();
                }
                yield return new WaitForSeconds(3.0f);
                item1014Spawned = false;
            }
        }
  

    }
    private IEnumerator Item1007_co()
    {
        bool item1007Spawned = false;
        if (!item1007Spawned)
        {
            item1007Spawned = true;
            for (int i = 0; i < 3; i++)
            {
                GameObject item1007 = Managers.Resource.Instantiate("Item1014Skill");
                item1007.transform.position = Player.transform.position;

                item1007.SetRandomPositionSphere(1, 1, 5);
                Debug.Log("위치 2개의 직선 을 이어주는 연기 필요");
                Debug.Log("연동방법   함수 (item1007.transform.position ,item1007.SetRandomPositionSphere(1, 1, 5) ");
                item1007.GetOrAddComponent<Item1007Skill>();
                
            }
            yield return new WaitForSeconds(3.0f);
            item1007Spawned = false;
        }

    }

    private IEnumerator Item1019_co()
    {
        bool item1019Spawned = false;
        if (!item1019Spawned)
        {
            
            Item1019Skill[] prev1019skills= GameObject.FindObjectsOfType<Item1019Skill>();
            if(prev1019skills.Length > 0)
            {
                for (int i = 0; i < prev1019skills.Length; i++)
                {
                    Managers.Resource.Destroy(prev1019skills[i].gameObject);
                }
            }



            item1019Spawned = true;
            for (int i = 0; i < (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count); i++)
            {
                GameObject item1019 = Managers.Resource.Instantiate("Item1019Skill");
                item1019.transform.position = Player.transform.position;
                item1019.SetRandomPositionSphere(1, 10, 5);
                item1019.GetOrAddComponent<Item1007Skill>();
               
            }
            yield return new WaitForSeconds(3.0f);
            item1019Spawned = false;
        }
    }
}
