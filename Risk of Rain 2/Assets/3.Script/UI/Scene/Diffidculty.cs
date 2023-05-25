using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Diffidculty : UI_Scene,IListener
{
    public Define.EDifficulty myDifficulty;
    enum EButtons
    {
        DifficultyButton
    }
    enum EImages
    {
        DifficultySelectEffect,
        Diffidculty,
    }
    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(EImages));
        Bind<Button>(typeof(EButtons));
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.CharacterSelectButton;
        Managers.Event.AddListener(Define.EVENT_TYPE.DifficultyChange, this);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation= Quaternion.identity;
        GetImage((int)EImages.DifficultySelectEffect).color = Color.white;
        
        switch (myDifficulty)
        {
            case Define.EDifficulty.Easy:
                GetButton((int)EButtons.DifficultyButton).GetComponent<Image>().sprite
                    = Managers.Resource.LoadSprte("Difficultyicon1");
                Debug.Log("게임 난이도 별 소리 설정하시려면 여기다");
                break;
            case Define.EDifficulty.Normal:
                GetButton((int)EButtons.DifficultyButton).GetComponent<Image>().sprite
             = Managers.Resource.LoadSprte("Difficultyicon2");
                break;
            case Define.EDifficulty.Hard:
                GetButton((int)EButtons.DifficultyButton).GetComponent<Image>().sprite
           = Managers.Resource.LoadSprte("Difficultyicon3");
                break;
        }
        GetButton((int)EButtons.DifficultyButton).gameObject
            .BindEvent((PointerEventData data) => DifficultyButtonClickEvent());
        GetButton((int)EButtons.DifficultyButton).gameObject
            .BindEvent((PointerEventData data) => DifficultyButtonPointerEnter(), Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.DifficultyButton).gameObject
           .BindEvent((PointerEventData data) => DifficultyButtonPointerExit(), Define.UIEvent.PointerExit);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        SetDifficultyImage();

    }
    private void DifficultyButtonClickEvent()
    {
        Managers.Game.Difficulty = myDifficulty;
        Managers.Event.PostNotification(Define.EVENT_TYPE.DifficultyChange, this);
   
    }
    private void DifficultyButtonPointerEnter()
    {
       
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerEnter, this);
    }
    private void DifficultyButtonPointerExit()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.MousePointerExit, this);
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.DifficultyChange:
                SetDifficultyImage();
                break;
        }
        
    }
    private void SetDifficultyImage()
    {
        if (!Managers.Game.Difficulty.Equals(myDifficulty))
        {
            GetImage((int)EImages.DifficultySelectEffect).color = Color.white;
        }
        else
        {
            GetImage((int)EImages.DifficultySelectEffect).color = Color.yellow;
        }
    }
}
