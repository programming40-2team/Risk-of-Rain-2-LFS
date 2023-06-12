using UnityEngine;

public class MonsterIneventoryPannel : MonoBehaviour
{
    void Start()
    {
        Init();
    }
    public void Init()
    {
        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach (int i in Managers.Data.MonData.Keys)
        {
            InvenMonsterButton monster = Managers.UI.ShowSceneUI<InvenMonsterButton>();
            monster.transform.SetParent(gameObject.transform);
            monster.MonsterCode = i;
        }

    }
}
