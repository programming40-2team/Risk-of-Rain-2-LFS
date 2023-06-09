using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameResultUI : UI_Game
{
    enum GameObjects
    {
        Victory,
        Default,

    }
    enum Texts
    {
        LastDifficultyTitle,
        LiveTimeTitle,
        LiveTimeText,
        KillTitle,
        KillText,
        AttackDamageTitle,
        AttackDamageText,
        HittedDamageTitle,
        HittedDamageText,
        LevelTitle,
        LevelText,
        GainGoldTItle,
        GainGoldText,
        CompleteStageTitle,
        CompleteStageText,
        TotalTitle,
        TotalText
    }
    enum Images
    {
        LastDifficultyImage
    }
    enum Buttons
    {
        BackButton
    }
    void Start()
    {
        Init(); ;
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        GetImage((int)Images.LastDifficultyImage).sprite = Managers.Resource.LoadSprte($"Difficultyicon{(int)Managers.Game.Difficulty + 1}");
        Get<GameObject>((int)GameObjects.Default).SetActive(false);
        Get<GameObject>((int)GameObjects.Victory).SetActive(false);
        if (Managers.Game.IsClear)
        {
            Get<GameObject>((int)GameObjects.Victory).SetActive(true);
        }
        else
        {
            Get<GameObject>((int)GameObjects.Default).SetActive(true);
        }
        GetComponent<Canvas>().sortingOrder = 9999;
        SetText();
        Time.timeScale = 0;

        GetButton((int)Buttons.BackButton).gameObject
            .BindEvent((PointerEventData data) => Managers.Scene.LoadScene(Define.Scene.MainMenu));

    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    private void SetText()
    {
        GameUI gameUI = FindObjectOfType<GameUI>();
        GetText((int)Texts.LastDifficultyTitle).text = "최고 도달 난이도";
        GetText((int)Texts.LiveTimeTitle).text = $"생존 시간:<color=#FFFF00>{gameUI.RunTime / 60:00}:{gameUI.RunTime % 60:00}</color>";
        GetText((int)Texts.LiveTimeText).text = $"<color=#FFFF00> {gameUI.RunTime:00}</color>점.";

        GetText((int)Texts.KillTitle).text = $"처치:<color=#FFFF00>{Managers.Game.KillCount}</color>";
        GetText((int)Texts.KillText).text = $"<color=#FFFF00>{Managers.Game.KillCount * 5}</color>점.";

        GetText((int)Texts.AttackDamageTitle).text = $"입힌 피해<color=#FFFF00>:{Managers.Game.MonsterDamaged} </color>";
        GetText((int)Texts.AttackDamageText).text = $"<color=#FFFF00>{Managers.Game.MonsterDamaged * 0.1f:00} </color>점.";

        GetText((int)Texts.HittedDamageTitle).text = $"받은 피해<color=#FFFF00>:{Managers.Game.PlayerAttackedDamage} </color>";
        GetText((int)Texts.HittedDamageText).text = $"<color=#FFFF00> {Managers.Game.PlayerAttackedDamage * 0.1f:00}</color>점.";
        if (GameObject.FindGameObjectWithTag("Player").TryGetComponent(out PlayerStatus status))
        {
            GetText((int)Texts.LevelTitle).text = $"최고 레벨<color=#FFFF00>:{status.Level} </color>";
            GetText((int)Texts.LevelText).text = $"<color=#FFFF00> {status.Level * 100}</color>점.";
        }

        GetText((int)Texts.GainGoldTItle).text = $"남은 골드<color=#FFFF00>:{Managers.Game.Gold}</color>";
        GetText((int)Texts.GainGoldText).text = $"<color=#FFFF00> {Managers.Game.Gold * 10:00}</color>점.";

        GetText((int)Texts.CompleteStageTitle).text = $"완료한 스테이지<color=#FFFF00>: {Managers.Game.StageNumber}</color>";
        GetText((int)Texts.CompleteStageText).text = $"<color=#FFFF00>  {Managers.Game.StageNumber * 10}</color>점.";

        GetText((int)Texts.TotalTitle).text = $"<color=#FFFF00><b>총점</b> : </color>";
        GetText((int)Texts.TotalText).text = $"<color=#FFFF00>  {Managers.Game.Gold * 10 + gameUI.RunTime + Managers.Game.KillCount * 5 + Managers.Game.MonsterDamaged * 0.1f + Managers.Game.PlayerAttackedDamage * 0.1f + Managers.Game.PlayerLevel * 100 + Managers.Game.StageNumber * 10:00}</color>점.";

    }

}
