using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//스킬은 데이터 구조를 짜고 들어가지 않아서 많이 난잡합니다...
public class LoadSkillTempo : MonoBehaviour
{
    [SerializeField] string skillKey;
    [SerializeField] public string skillTitle;
    [SerializeField] public string skillContents;
    void Start()
    {
        gameObject.BindEvent((PointerEventData data) => MousePointerEnterEvent(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => MousePointerExitEvent(), Define.UIEvent.PointerExit);
    }

    private void MousePointerEnterEvent()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerEnter, this);
    }
    private void MousePointerExitEvent()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerExit, this);
    }
}
