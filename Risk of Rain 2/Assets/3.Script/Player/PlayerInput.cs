using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string MoveAxisName = "Vertical";
    public string RotateAxisName = "Horizontal";

    public float Move { get; private set; }
    public float Rotate { get; private set;}

    private void Update()
    {
        Move = Input.GetAxis(MoveAxisName);
        Rotate = Input.GetAxis(RotateAxisName);
    }
}
