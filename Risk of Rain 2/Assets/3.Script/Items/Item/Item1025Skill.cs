using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1025Skill : ItemPrimitiive
{
    Item1025Skill[] Item1025;

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Item1025 = FindObjectsOfType<Item1025Skill>();
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKey(KeyCode.E))
            {

                
                for(int i = 0; i < Item1025.Length; i++)
                {
                    if (Item1025[i].gameObject.GetHashCode() != gameObject.GetHashCode())
                    {
                        Player.transform.position = Item1025[i].transform.position;
                        break;
                    }
                }


            }

        }

    }


    private void OnTriggerExit(Collider other)
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);


    }

}
