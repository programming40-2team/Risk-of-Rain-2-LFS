using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : UI_Scene
{

    public int Charactercode = -1;
   
    enum Images
    {
        CharacterImage,

    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = 15;
        Bind<Image>(typeof(Images));
        GetImage((int)Images.CharacterImage).sprite
            = Managers.Resource.Load<Sprite>(Managers.Data.CharacterDataDict[Charactercode].iconkey);
    }



}
