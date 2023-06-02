using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1025Skill : ItemPrimitiive
{
    private int _teleCode = -1;
    public int TeleCode => _teleCode;
    Item1025Skill[] Item1025;

    private bool isMoving = false;
    public void SetTeleCode(int n)
    {
        _teleCode = n;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Item1025 = FindObjectsOfType<Item1025Skill>();
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKey(KeyCode.E)&&!isMoving)
            {
                isMoving = true;
                for(int i = 0; i < Item1025.Length; i++)
                {
                    if (Item1025[i].TeleCode!=_teleCode)
                    {
                        Debug.Log("내 텔레코드 : " + TeleCode);
                        Debug.Log("통과한 텔레코드 : "+Item1025[i].TeleCode);
                        Player.transform.position = Item1025[i].transform.position;
                        break;
                    }
                }


            }

            isMoving = false;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);


    }

}
