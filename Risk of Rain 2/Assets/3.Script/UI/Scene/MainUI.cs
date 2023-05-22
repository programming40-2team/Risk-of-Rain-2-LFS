using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : UI_Scene
{
    [SerializeField]
    private string _username = "Noname";
    GameObject MouseCursor;


    enum Buttons
    {
        GameStartButton,
        DicitonaryButton,
        MusicButton,
        SettingButton,
        QuitButton,
        UserProfileButton,

    }
    enum Images
    {
        BackGround,
        MainTitle,

    }
    enum Texts
    {
        UserProfileText,
        GameStartText,
        DicitonaryText,
        MusicText,
        SettingText,
        QuitText,

    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));


        GetText((int)Texts.UserProfileText).text = $"프로필 :{_username}";
        GetText((int)Texts.GameStartText).text = $"게임시작";
        GetText((int)Texts.DicitonaryText).text = $"로그북";
        GetText((int)Texts.MusicText).text = $"음악";
        GetText((int)Texts.SettingText).text = $"설정";
        GetText((int)Texts.QuitText).text = $"데스크톱으로 나가기";

        Debug.Log("게임 실행 하면 나오는 배경음 나오는 곳!");
        GetButton((int)Buttons.GameStartButton).gameObject
            .BindEvent((PointerEventData data) => GameStartEvent());
        GetButton((int)Buttons.DicitonaryButton).gameObject
            .BindEvent((PointerEventData data) => ShowLogBook());
    }
    private void Start()
    {
        Init();
    }
    private void GameStartEvent()
    {
        Debug.Log("게임 시작 버튼 누르면 나올 소리 여기다!");

        Managers.UI.ShowSceneUI<GameStartUI>();

    }
    private void ShowLogBook()
    {
        Debug.Log("로그북 버튼 누르면 나올 소리 여기다!");
        Managers.UI.ShowSceneUI<LogBook>();
    }


}
