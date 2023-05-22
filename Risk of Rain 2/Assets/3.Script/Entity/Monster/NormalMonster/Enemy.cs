//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    Transform targets;
//    Animator animator;
//    float enemyMoveSpeed = 10f;

//    private Rigidbody _enemyRigidbody;

//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//        StartCoroutine(UpdatetargetCoroutine()); 
//        TryGetComponent(out _enemyRigidbody);
//    }
//    private IEnumerator UpdatetargetCoroutine()
//    {
//        while (true)
//        {
//            UpdateTarget();
//            yield return new WaitForSeconds(0.25f);
//        }
//    }

//    private void UpdateTarget()
//    {
//        Collider[] Cols = Physics.OverlapSphere(transform.position, 10f);
//        Debug.Log(Physics.OverlapSphere(transform.position, 10f, 1 << 8));
//        if (Cols.Length > 0)
//        {
//            float distance = Mathf.Infinity;
//            for (int i = 0; i < Cols.Length; i++)
//            {
//                if (Cols[i].tag == "Player")
//                {
//                    Debug.Log("Physic Enemy : Target found");
//                    float newDistance = Vector3.Distance(transform.position, Cols[i].transform.position);
//                    if (newDistance < distance)
//                    {
//                        distance = newDistance;
//                        targets = Cols[i].gameObject.transform;
//                    }
//                }
//            }
//        }
//    }
//    private void Update()
//    {
//        if (targets != null)
//        {
//            targets.position = new Vector3(targets.position.x, transform.position.y, targets.position.z);
//            Vector3 dir = targets.position - transform.position;
//            animator.SetBool("Sprint", true);
//            transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime, Space.World);
//            _enemyRigidbody.MovePosition(transform.position);
//            transform.LookAt(targets.position);
//        }
//        else
//        {
//            Debug.Log("인식을 못함");
//            animator.SetBool("Sprint", false);
//        }
//    }
//    private void OnTriggerStay(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            targets.position= new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
//            transform.LookAt(targets.position);
//        }
//    }
//}

