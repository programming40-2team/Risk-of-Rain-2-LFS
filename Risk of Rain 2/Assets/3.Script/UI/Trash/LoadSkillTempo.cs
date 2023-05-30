using UnityEngine;
using UnityEngine.EventSystems;

//스킬은 데이터 구조를 짜고 들어가지 않아서 많이 난잡합니다...
public class LoadSkillTempo : MonoBehaviour, IListener
{
    [SerializeField] Define.ESkillType SkillType;
    public string skillTitle;
    public string skillContents;
    //private int characterCode;
    void Start()
    {
        gameObject.BindEvent((PointerEventData data) => MousePointerEnterEvent(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => MousePointerExitEvent(), Define.UIEvent.PointerExit);
        Managers.Event.AddListener(Define.EVENT_TYPE.SelectCharacter, this);
    }

    private void MousePointerEnterEvent()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerEnter, this);
    }
    private void MousePointerExitEvent()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerExit, this);
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        if (Sender.TryGetComponent(out CharacterSelectButton Char))
        {
            // characterCode = Char.Charactercode;
            ChangeTitle(Char.Charactercode);
        }

    }
    private void ChangeTitle(int charcode)
    {

        switch (SkillType)
        {
            case Define.ESkillType.Passive:
                skillTitle = Managers.Data.CharacterDataDict[charcode].passiveskill;
                skillContents = Managers.Data.CharacterDataDict[charcode].passiveskillscript;
                break;
            case Define.ESkillType.M1:
                skillTitle = Managers.Data.CharacterDataDict[charcode].m1skill;
                skillContents = Managers.Data.CharacterDataDict[charcode].m1skillscript;
                break;
            case Define.ESkillType.M21:
                skillTitle = Managers.Data.CharacterDataDict[charcode].m2skill_1;
                skillContents = Managers.Data.CharacterDataDict[charcode].m2skill_1script;
                break;
            case Define.ESkillType.M22:
                skillTitle = Managers.Data.CharacterDataDict[charcode].m2skill_2;
                skillContents = Managers.Data.CharacterDataDict[charcode].m2skill_2script;
                break;
            case Define.ESkillType.Shift1:
                skillTitle = Managers.Data.CharacterDataDict[charcode].shiftskill_1;
                skillContents = Managers.Data.CharacterDataDict[charcode].shiftskill_2script;
                break;
            case Define.ESkillType.Shift2:
                skillTitle = Managers.Data.CharacterDataDict[charcode].shiftskill_2;
                skillContents = Managers.Data.CharacterDataDict[charcode].shiftskill_2script;
                break;
            case Define.ESkillType.R1:
                skillTitle = Managers.Data.CharacterDataDict[charcode].rskill_1;
                skillContents = Managers.Data.CharacterDataDict[charcode].rskill_1script;
                break;
            case Define.ESkillType.R2:
                skillTitle = Managers.Data.CharacterDataDict[charcode].r_skill2;
                skillContents = Managers.Data.CharacterDataDict[charcode].r_skill2script;
                break;
        }
    }
}
