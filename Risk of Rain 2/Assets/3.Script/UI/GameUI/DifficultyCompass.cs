using UnityEngine;

public class DifficultyCompass : MonoBehaviour
{
    private bool isFirst = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFirst)
        {
            isFirst = true;
            Debug.Log($"현재 난이도:{Managers.Game.Difficulty}");
        }
        else
        {
            Managers.Game.Difficulty = (Define.EDifficulty)((int)Managers.Game.Difficulty + 1);
            Debug.Log($"현재 난이도:{Managers.Game.Difficulty}");
        }

    }
}
