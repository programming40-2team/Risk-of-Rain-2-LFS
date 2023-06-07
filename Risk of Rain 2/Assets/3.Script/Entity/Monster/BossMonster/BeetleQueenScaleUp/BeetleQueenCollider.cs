using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BeetleQueenCollider : MonoBehaviour
{
    private SkinnedMeshRenderer _meshRenderer;
    private MeshCollider _collider;
    private Entity _meshEntity;

    private float _timer = 0;
    private float _updateTime = 0.25f;

    private void Awake()
    {
        TryGetComponent(out _meshRenderer);
        TryGetComponent(out _collider);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _updateTime)
        {
            _timer = 0;
            UpdateCollider();
        }
    }
    
    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        _meshRenderer.BakeMesh(colliderMesh);
        _collider.sharedMesh = null;
        _collider.sharedMesh = colliderMesh;
    }
    
}