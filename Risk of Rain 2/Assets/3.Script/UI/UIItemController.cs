using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemController : MonoBehaviour
{
    public GameObject tagertObject; // 큐브
    [SerializeField] private float rotateSpeed;

    private void Start()
    {
        // 큐브의 크기 측정
        Vector3 cubeSize = GetObjectSize(tagertObject);

        // 작은 물체의 크기 조정
        SetObjectSize(gameObject, cubeSize);
        rotateSpeed = 50.0f;
        Debug.DrawRay(gameObject.transform.position, 15000*Vector3.forward, Color.red);
        gameObject.transform.GetChild(0).transform.localPosition = Vector3.zero;
    }
    private void Update()
    {
     
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        // gameObject.transform.Rotate(Vector3.up,Time.deltaTime*rotateSpeed); 
    }
    // 물체의 크기 측정
    private Vector3 GetObjectSize(GameObject obj)
    {
        Renderer renderer = obj.GetComponentInChildren<Renderer>();
        Vector3 size = renderer.bounds.size;
        return size;
    }

    // 물체의 크기 조정
    private void SetObjectSize(GameObject obj, Vector3 size)
    {
        Vector3 originalSize = GetObjectSize(obj);
        Vector3 scale = obj.transform.localScale;

        scale.x *= size.x / originalSize.x;
        scale.y *= size.y / originalSize.y;
        scale.z *= size.z / originalSize.z;

        obj.transform.localScale = scale;
    }
}
