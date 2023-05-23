using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public Define.EDifficulty Difficulty { get; set; } = Define.EDifficulty.Easy;
    public int Gold { get; set; } = 0;
    public int StageNumber { get; set; } = 1;
    public int StageLevel { get; set; } = 1;

    public int PlayerLevel { get; set; } = 1;
    public bool IsTelePortActive { get; set; } = false;

}
