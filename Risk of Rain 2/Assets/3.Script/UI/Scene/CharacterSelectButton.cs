using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButton : UI_Scene, IListener
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


        GetImage((int)EImages.CharacterImage).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[Charactercode].iconkey);
        gameObject.GetComponent<Button>().enabled = false;
        if (Charactercode.Equals(7))
        {
            Color color = Color.white;
            color.a = 1;
            GetImage((int)EImages.CharacterImage).color = color;
            gameObject.GetComponent<Button>().enabled = true;

            gameObject.BindEvent((PointerEventData data) => EventExcute());
        }
        else if (Charactercode.Equals(1))
        {
            Color color = Color.yellow;
            color.a = 1;
            GetImage((int)EImages.CharacterImage).color = color;
            gameObject.GetComponent<Button>().enabled = true;

            gameObject.BindEvent((PointerEventData data) => EventExcute());
        }

        selectImageprevcolor = GetImage((int)EImages.SelectChangeColorImage).color;
        rectImageprevcolor = GetImage((int)EImages.Character_RectImage).color;
        gameObject.BindEvent((PointerEventData data) => CharacterPointerEnterEvent(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => CharacterPointerExitEvent(), Define.UIEvent.PointerExit);

        Managers.Event.AddListener(Define.EVENT_TYPE.SelectCharacter, this);
    }
    private void CharacterPointerEnterEvent()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerEnter, this);
        GetImage((int)EImages.Character_RectImage).color = Color.white;
        Debug.Log("캐릭터 화면 선택 하는 효과음");
        SoundManager.instance.PlaySE("MenuHover");
    }
    private void CharacterPointerExitEvent()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerExit, this);
        GetImage((int)EImages.Character_RectImage).color = rectImageprevcolor;
    }
    private void ExchangeEffectOfCharacterSelectButton()
    {
        if (Get<GameObject>((int)EGameObjects.Character_RectImage_Image).activeSelf)
        {

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

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        if (Sender.TryGetComponent(out CharacterSelectButton button))
        {
            if (!button.Charactercode.Equals(Charactercode))
            {
                GetImage((int)EImages.SelectChangeColorImage).color = selectImageprevcolor;
            }
            else
            {
                GetImage((int)EImages.SelectChangeColorImage).color = Color.red;
            }
        }
    }
}
