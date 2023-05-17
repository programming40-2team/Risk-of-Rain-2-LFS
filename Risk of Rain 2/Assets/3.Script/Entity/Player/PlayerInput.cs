using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public string MoveAxisName = "Vertical";
    public string HorizontalAxisName = "Horizontal";
    
    public float Move { get; private set; }
    public float HorizontalDirection { get; private set;}
    
    private void Start()
    {
        Cursor.visible = false;
        TryGetComponent(out _playerMovement);
    }
    private void Update()
    {
        Move = Input.GetAxis(MoveAxisName);
        HorizontalDirection = Input.GetAxis(HorizontalAxisName);
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _playerMovement.Jump();
        }
    }
}
