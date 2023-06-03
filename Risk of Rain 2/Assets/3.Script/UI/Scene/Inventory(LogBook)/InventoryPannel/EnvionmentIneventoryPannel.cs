using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnvionmentIneventoryPannel : MonoBehaviour
{
    private int CurrentIndex = 0;
    private List<int> EnvDataKeyList = new List<int>();
    void Start()
    {
        EnvDataKeyList = Managers.Data.EnvDataDict.Keys.ToList();
        Init();
    }
    public void Init()
    {

        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        for (int i = CurrentIndex; i < CurrentIndex + 4; i++)
        {
            EnvButton item = Managers.UI.ShowSceneUI<EnvButton>();
            item.transform.SetParent(gameObject.transform);
            item.EnvCode = EnvDataKeyList[i % EnvDataKeyList.Count];

        }
        CurrentIndex += 4;
    }
}
