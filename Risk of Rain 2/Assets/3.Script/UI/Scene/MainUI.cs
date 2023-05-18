using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    enum GameObjects
    {
        MouseCursor
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
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        MouseCursor = Get<GameObject>((int)GameObjects.MouseCursor);

        GetText((int)Texts.UserProfileText).text = $"프로필 :{_username}";
        GetText((int)Texts.GameStartText).text = $"게임시작";
        GetText((int)Texts.DicitonaryText).text = $"로그북";
        GetText((int)Texts.MusicText).text = $"음악";
        GetText((int)Texts.SettingText).text = $"설정";
        GetText((int)Texts.QuitText).text = $"데스크톱으로 나가기";
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
       

    }


}
