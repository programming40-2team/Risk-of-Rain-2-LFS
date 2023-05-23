using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InteractionWithItemContainer Interaction= Managers.UI.MakeWorldSpaceUI<InteractionWithItemContainer>();
        Interaction.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
