using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButton : UI_Scene
{

    public int Charactercode = -1;
    Color prevcolor;
    enum Images
    {
        CharacterImage,
        SelectChangeColorImage,


    }
    enum GameObjects
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
        GetComponent<Canvas>().sortingOrder = 15;
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GetImage((int)Images.CharacterImage).sprite= Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[Charactercode].iconkey);
        gameObject.GetComponent<Button>().enabled = false;
        if (Charactercode.Equals(7))
        {
            Color color = Color.white;
            color.a = 1;    
            GetImage((int)Images.CharacterImage).color = color;
            gameObject.GetComponent<Button>().enabled = true;

            gameObject.BindEvent((PointerEventData data) => EventExcute());
        }
      
        prevcolor = GetImage((int)Images.SelectChangeColorImage).color;
    }
    private void ExchangeEffectOfCharacterSelectButton()
    {
        if (Get<GameObject>((int)GameObjects.Character_RectImage_Image).activeSelf)
        {
            GetImage((int)Images.SelectChangeColorImage).color =
                Color.red;

            Get<GameObject>((int)GameObjects.Character_RectImage_Image).SetActive(false);
        }
        else
        {
            GetImage((int)Images.SelectChangeColorImage).color = prevcolor;
            Get<GameObject>((int)GameObjects.Character_RectImage_Image).SetActive(true);
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
