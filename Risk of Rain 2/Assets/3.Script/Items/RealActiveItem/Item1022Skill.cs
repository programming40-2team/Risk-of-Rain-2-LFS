using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1022Skill : ItemPrimitiive
{
    private float movespeed = 8f;
    private bool IsCooltime = false;
    private float force = 5.0f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out Entity entity))
            {
               
                    StartCoroutine(nameof(Item1022_co), entity);
                
            }
       
        }
    }
    private void Start()
    {
        base.Init();
        Vector3 spawnpos = new Vector3(Player.transform.forward.x + Player.transform.up.x*0.3f + gameObject.transform.position.x,
     Player.transform.forward.y + Player.transform.up.y*0.3f + gameObject.transform.position.y,
     Player.transform.forward.z + Player.transform.up.z*0.3f + gameObject.transform.position.z);
        gameObject.transform.position =Player.transform.position+2*Player.transform.forward;
        GetComponent<Rigidbody>().velocity = (Player.transform.forward+0.4f*Player.transform.up).normalized* movespeed;
    
        Managers.Resource.Destroy(gameObject, 10.0f);
    }



    private IEnumerator Item1022_co(Entity go)
    {
        IsCooltime = true;

        //Vector3 movedir = gameObject.transform.position - go.transform.position;
        if(go.TryGetComponent(out Rigidbody rigid))
        {
            rigid.AddForce((gameObject.transform.position-rigid.transform.position).normalized* force);
        }
        go.OnDamage(10f);
        yield return new WaitForSeconds(0.1f);
        IsCooltime = false;
    }
    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(nameof(Item1022_co));
        if(other.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}
