using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithItemContainer : UI_Scene
{

    private void Start()
    {
        gameObject.transform.GetChild(0).localPosition= Vector3.zero;
        Debug.Log("원하는 위치에 UI 옮겨놓는 기능 ");
        Debug.Log(" 이 게임은 상호작용이 캐릭터 우측에 나타나므로, 캐릭터 기준으로 Bownder+ 하드코딩 값 A 더해서 나타내면 될듯? ");

    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        gameObject.transform.GetChild(0).transform.localScale = 5*Vector3.one * (1 / parent.localScale.x);
    }
}
