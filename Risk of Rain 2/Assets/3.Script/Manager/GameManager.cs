
public class GameManager
{
    private Define.EDifficulty _difficulty = Define.EDifficulty.Easy;
    private Define.EGameState _gameState = Define.EGameState.NonTelePort;
    private int _gold = 0;
    private bool _isclear = false;
    private Define.WhenItemActivates _playerState=Define.WhenItemActivates.Always;


    #region 마지막 통계를 위함
    private int _killCount = 0;
    private float _monsterDamaged = 0;
    private float _playerAttackedDamage = 0;
    public int KillCount
    {
        get { return _killCount; }
        set { _killCount = value; }
    }
    public float MonsterDamaged
    {
        get { return _monsterDamaged; }
        set { _monsterDamaged = value; }
    }
    public float PlayerAttackedDamage
    {
        get { return _playerAttackedDamage; }
        set { _playerAttackedDamage = value; }
    }

    //캐릭터가 죽으면 혹은 클리어 조건을 달성했다면 한번만 변경해주세요!
    public bool IsClear
    {
        get { return _isclear; }
        set
        {
            _isclear = value;
            Managers.UI.ShowPopupUI<GameResultUI>();

        }
    }

    #endregion
    public Define.EDifficulty Difficulty
    {
        get { return _difficulty; }
        set
        {
            _difficulty = value;
            Managers.Event.DifficultyChange?.Invoke((int)_difficulty);
        }
    }
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            Managers.Event.GoldChange?.Invoke(_gold);
        }
    }

    public int StageNumber { get; set; } = 1;
    public int StageLevel { get; set; } = 1;

    public int PlayerLevel { get; set; } = 1;
    public Define.EGameState GameState
    {
        get { return _gameState; }
        set
        {
            _gameState = value;
            // 보스 소환시 나타날 이벤트들 로 일단 임시
            // 추후 상의 후 어디다가 보스 생성 이벤트들을 어디서 어떻게 연동시켜줄지 고려
            Managers.Event.GameStateChange?.Invoke();
        }
    }

    public Define.WhenItemActivates PlayerState
    {
        get
        {
            return _playerState;
        }
        set
        {
            _playerState = value;

        }
    }

    public float PlayingTIme { get; set; } = 0;


}