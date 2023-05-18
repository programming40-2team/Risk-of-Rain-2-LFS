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
        for(int i = 0; i < Define.MaxCharacterCount; i++)
        {
            CharacterSelectButton Character = Managers.UI.ShowSceneUI<CharacterSelectButton>();
            Character.transform.SetParent(gameObject.transform);
            Character.Charactercode = Managers.Data.CharacterDataDict[i+1].charatercode;
        }
    }

}
