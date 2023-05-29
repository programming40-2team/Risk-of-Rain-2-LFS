using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassiveItem 
{
    void OnExcute(int itemcode, Define.WhenItemActivates WhenitemActivates);

}
