using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public interface IListener
{
    //Notification function to be invoked on Listeners when events happen
    void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);
}