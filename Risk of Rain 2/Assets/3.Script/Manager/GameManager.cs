using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    private Define.EDifficulty _difficulty = Define.EDifficulty.Easy;
    private bool _isTelePortActive = false;
    private int _gold = 0;





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
    public bool IsTelePortActive
    {
        get { return _isTelePortActive;}
        set
        {
            _isTelePortActive = value;
           // 보스 소환시 나타날 이벤트들 로 일단 임시
           // 추후 상의 후 어디다가 보스 생성 이벤트들을 어디서 어떻게 연동시켜줄지 고려
        }
    }
    public float PlayingTIme { get; set; } = 0;


}
