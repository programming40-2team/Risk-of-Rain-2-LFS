using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenCharacterButton : UI_Scene
{

    public int Charactercode = -1;

    enum EImages
    {
        CharacterImage,
        SelectChangeColorImage,

        Character_RectImage,
        IsHaveCharacter,
    }
    enum EGameObjects
    {
        Character_RectImage_Image,

    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.CharacterSelectButton;
        Bind<Image>(typeof(EImages));
        Bind<GameObject>(typeof(EGameObjects));
        Get<GameObject>((int)EGameObjects.Character_RectImage_Image).SetActive(false);


    }

    private void SetImage()
    {
        GetImage((int)EImages.CharacterImage).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[Charactercode].iconkey);

        if (!Managers.Data.CharacterDataDict[Charactercode].isActive)
        {
            GetImage((int)EImages.IsHaveCharacter).color = Color.red;
        }
        else
        {

        }
    }




}
