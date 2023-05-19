using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButton : UI_Scene
{

    public int Charactercode = -1;
    Color selectImageprevcolor;
    Color rectImageprevcolor;
    enum EImages
    {
        CharacterImage,
        SelectChangeColorImage,

        Character_RectImage,

    }
    enum EGameObjects
    {
        Character_RectImage_Image,

    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.CharacterSelectButton;
        Bind<Image>(typeof(EImages));
        Bind<GameObject>(typeof(EGameObjects));
     

        GetImage((int)EImages.CharacterImage).sprite= Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[Charactercode].iconkey);
        gameObject.GetComponent<Button>().enabled = false;
        if (Charactercode.Equals(7))
        {
            Color color = Color.white;
            color.a = 1;    
            GetImage((int)EImages.CharacterImage).color = color;
            gameObject.GetComponent<Button>().enabled = true;

            gameObject.BindEvent((PointerEventData data) => EventExcute());
        }

        selectImageprevcolor = GetImage((int)EImages.SelectChangeColorImage).color;
        rectImageprevcolor = GetImage((int)EImages.Character_RectImage).color;
        gameObject.BindEvent((PointerEventData data) => CharacterPointerEnterEvent(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => CharacterPointerExitEvent(), Define.UIEvent.PointerExit);
    }
    private void CharacterPointerEnterEvent()
    {
        GetImage((int)EImages.Character_RectImage).color = Color.white;
        Debug.Log("캐릭터 화면 선택 하는 과정에서 효과음 넣으실껀가요?1");
    }
    private void CharacterPointerExitEvent()
    {
        GetImage((int)EImages.Character_RectImage).color = rectImageprevcolor;
        Debug.Log("캐릭터 화면 선택 하는 과정에서 효과음 넣으실껀가요?2");
    }
    private void ExchangeEffectOfCharacterSelectButton()
    {
        if (Get<GameObject>((int)EGameObjects.Character_RectImage_Image).activeSelf)
        {
            GetImage((int)EImages.SelectChangeColorImage).color =
                Color.red;

            Get<GameObject>((int)EGameObjects.Character_RectImage_Image).SetActive(false);
        }
        else
        {
            GetImage((int)EImages.SelectChangeColorImage).color = selectImageprevcolor;
            Get<GameObject>((int)EGameObjects.Character_RectImage_Image).SetActive(true);
        }
       
    }
    private void EventExcute()
    {
        ExchangeEffectOfCharacterSelectButton();
        Debug.Log($"{Charactercode}가 난 선택돼써 이벤트 발송!");
        Debug.Log("캐릭터가 선택되었습니다. 캐릭터 설정 소리는 여기다가.!");
        Managers.Event.PostNotification(Define.EVENT_TYPE.SelectCharacter, this);
    }



}
