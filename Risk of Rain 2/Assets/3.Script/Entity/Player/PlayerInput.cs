using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public string MoveAxisName = "Vertical";
    public string RotateAxisName = "Horizontal";
    
    public float Move { get; private set; }
    public float Rotate { get; private set;}
    
    private void Start()
    {
        Cursor.visible = false;
        TryGetComponent(out _playerMovement);
    }
    private void Update()
    {
        Move = Input.GetAxis(MoveAxisName);
        Rotate = Input.GetAxis(RotateAxisName);
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _playerMovement.Jump();
        }
    }
}
