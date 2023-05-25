using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterintroductionPannel : MonoBehaviour
{
    // Start is called before the first frame update
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
        foreach(var i in Managers.Data.CharacterDataDict.Keys)
        {
            CharacterSelectButton Character = Managers.UI.ShowSceneUI<CharacterSelectButton>();
            Character.transform.SetParent(gameObject.transform);
            Character.Charactercode = i;
        }
    }

}
