using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _verticalAxis = "Vertical";
    private string _HorizontalAxis = "Horizontal";
    private string _jump = "Jump";

    public float Move { get; private set; }
    public float HorizontalDirection { get; private set; }

    public bool Jump { get; private set; }
    public bool Sprint { get; private set; }
    public bool Mouse1 { get; private set; }
    public bool Mouse2 { get; private set; }
    public bool Shift { get; private set; }
    public bool Special { get; private set; }
    public bool ActiveItem { get; private set; }


    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        Move = Input.GetAxis(_verticalAxis);
        HorizontalDirection = Input.GetAxis(_HorizontalAxis);
        Jump = Input.GetButtonDown(_jump);
        Sprint = Input.GetKeyDown(KeyCode.LeftControl);
        Mouse1 = Input.GetMouseButton(0);
        Mouse2 = Input.GetMouseButton(1);
        Shift = Input.GetKeyDown(KeyCode.LeftShift);
        Special = Input.GetKeyDown(KeyCode.R);
        ActiveItem = Input.GetKeyDown(KeyCode.Q);
    }
}
