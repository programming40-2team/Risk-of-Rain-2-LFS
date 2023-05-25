using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIneventoryPannel : MonoBehaviour
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
            InvenCharacterButton Character = Managers.UI.ShowSceneUI<InvenCharacterButton>();
            Character.transform.SetParent(gameObject.transform);
            Character.Charactercode = i;
        }
    }
}
