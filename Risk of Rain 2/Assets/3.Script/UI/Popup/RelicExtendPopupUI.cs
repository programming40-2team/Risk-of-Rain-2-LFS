using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RelicExtendPopupUI : UI_Popup
{
    public EClickType MyType { get; set; }

    public enum EGameObjects
    {
        ClosePannel,
    }
   public enum EClickType
    {
        Relic,
        Extend,

    }
    enum ETexts
    {
        TitleText,
        TitleContentsText,
        DescribeText,

    }
    enum EButtons
    {
        ClosePopupButton,

    }
    public void SetText(EClickType clickType)
    {
        switch (clickType)
        {
            case EClickType.Relic:
                GetText((int)ETexts.TitleText).text = "유물";
                GetText((int)ETexts.TitleContentsText).text = "<color=#8A2BE2>유물</color>은 게임 프레이를 크게 뒤바꿔 놓는 게임 변조기 입니다. <color=#8A2BE2>유물</color>은 월드안의 <color=#8A2BE2>비밀 장소</color>에 숨겨져 있습니다.\n 유물이 활성화되어 있어도 도전을 잠금 해제할 수 있습니다.";
                GetText((int)ETexts.DescribeText).text = "이용 가능한 유물이 없습니다.";
                break;
            case EClickType.Extend:
                GetText((int)ETexts.TitleText).text = "확장팩";
                GetText((int)ETexts.TitleContentsText).text = "이번 게임에서 게임 확장팩이 사용됩니다.";
                GetText((int)ETexts.DescribeText).text = "이용 가능한 확장팩이 없습니다.";
                break;
        }
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));
        Bind<GameObject>(typeof(EGameObjects));
        Get<GameObject>((int)EGameObjects.ClosePannel).BindEvent((PointerEventData data) => Managers.UI.ClosePopupUI());
        GetButton((int)EButtons.ClosePopupButton).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ClosePopupUI());
        SetText(MyType);

    }
    private void Start()
    {
        Init();
    }




}
