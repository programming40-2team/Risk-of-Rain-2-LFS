using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.UI.ShowSceneUI<GameUI>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
