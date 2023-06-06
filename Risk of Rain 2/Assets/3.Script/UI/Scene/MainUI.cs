using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUI : UI_Scene
{
    [SerializeField]
    private string _username = "Noname";

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
        Bind<Image>(typeof(Images));

        GetText((int)Texts.UserProfileText).text = $"프로필 :{_username}";
        GetText((int)Texts.GameStartText).text = $"게임시작";
        GetText((int)Texts.DicitonaryText).text = $"로그북";
        GetText((int)Texts.MusicText).text = $"음악";
        GetText((int)Texts.SettingText).text = $"설정";
        GetText((int)Texts.QuitText).text = $"데스크톱으로 나가기";

        Debug.Log("게임 실행 하면 나오는 배경음 나오는 곳!");
        SoundManager.instance.PlayBGM("MainBgm");
        GetButton((int)Buttons.GameStartButton).gameObject
            .BindEvent((PointerEventData data) => GameStartEvent());
        GetButton((int)Buttons.DicitonaryButton).gameObject
            .BindEvent((PointerEventData data) => ShowLogBook());
        GetImage((int)Images.BackGround).gameObject.SetActive(false);
    }
    private void Start()
    {
        Init();
    }
    private void GameStartEvent()
    {
        Debug.Log("게임 시작 버튼 누르면 나올 소리 여기");
        SoundManager.instance.PlaySE("MenuClick");
        TurnOnandOffLog();
        Managers.UI.ShowSceneUI<GameStartUI>();

    }
    private void ShowLogBook()
    {
        Debug.Log("로그북 버튼 누르면 나올 소리 여기");
        SoundManager.instance.PlaySE("MenuClickLog");
        Managers.UI.ShowSceneUI<LogBook>();
    }
    public void TurnOnandOffLog()
    {
        if (GetImage((int)Images.MainTitle).enabled)
        {
            GetImage((int)Images.MainTitle).enabled = false;
        }
        else
        {
            GetImage((int)Images.MainTitle).enabled = true;
        }
    }


}
