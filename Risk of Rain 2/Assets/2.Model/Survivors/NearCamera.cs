using System.Collections.Generic;
using UnityEngine;

public class NearCamera : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Transform[] _childrenArr;
    private List<Renderer> _playerRendererList;
    private List<Material> _playerMaterialList;
    private List<Material> _translucentMaterial;

    private void Awake()
    {
        _playerRendererList = new List<Renderer>();
        _playerMaterialList = new List<Material>();
        _translucentMaterial = new List<Material>();
    }
    private void Start()
    {
        _childrenArr = _player.GetComponentsInChildren<Transform>();
        foreach (Transform child in _childrenArr)
        {
            if (child.TryGetComponent(out Renderer childRenderer))
            {
                _playerRendererList.Add(childRenderer);
                _playerMaterialList.Add(childRenderer.material);
            }
        }
        foreach (Material material in _playerMaterialList)
        {
            Material temp = material;
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 0.5f);
            _translucentMaterial.Add(temp);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TranslucentMaterial();
            Debug.Log("투명");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ResetMaterial();
            Debug.Log("복구");
        }

    }
    private void TranslucentMaterial()
    {
        for (int i = 0; i < _playerRendererList.Count; i++)
        {
            _playerRendererList[0].material = _translucentMaterial[0];
        }

        foreach (Transform child in _childrenArr)
        {
            if (child.TryGetComponent(out Renderer childRenderer))
            {
                childRenderer.material = _translucentMaterial[0];
            }
        }
    }

    private void ResetMaterial()
    {
        for (int i = 0; i < _playerMaterialList.Count; i++)
        {
            _playerRendererList[0].material = _playerMaterialList[0];
        }
    }

}
