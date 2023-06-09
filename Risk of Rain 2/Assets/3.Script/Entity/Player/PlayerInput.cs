using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _verticalAxis = "Vertical";
    private string _HorizontalAxis = "Horizontal";
    private string _jump = "Jump";

    public float Move { get; private set; }
    public float HorizontalDirection { get; private set; }
    public float MoveRaw { get; private set; }
    public float HorizontalDirectionRaw { get; private set; }

    public bool Jump { get; private set; }
    public bool Sprint { get; private set; }
    public bool Mouse1 { get; private set; }
    public bool Mouse2 { get; private set; }
    public bool Mouse1Down { get; private set; }
    public bool Mouse2Down { get; private set; }
    public bool Mouse1Up { get; private set; }
    public bool Mouse2Up { get; private set; }
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
        MoveRaw = Input.GetAxisRaw(_verticalAxis);
        HorizontalDirectionRaw = Input.GetAxisRaw(_HorizontalAxis);
        Jump = Input.GetButtonDown(_jump);
        Sprint = Input.GetKeyDown(KeyCode.LeftControl);
        Mouse1 = Input.GetMouseButton(0);
        Mouse2 = Input.GetMouseButton(1);
        Mouse1Down = Input.GetMouseButtonDown(0);
        Mouse2Down = Input.GetMouseButtonDown(1);
        Mouse1Up = Input.GetMouseButtonUp(0);
        Mouse2Up = Input.GetMouseButtonUp(1);
        Shift = Input.GetKeyDown(KeyCode.LeftShift);
        Special = Input.GetKeyDown(KeyCode.R);
        ActiveItem = Input.GetKeyDown(KeyCode.Q);
    }

    private void OnDisable()
    {
        Move = 0f;
        HorizontalDirection = 0f;
        MoveRaw = 0f;
        HorizontalDirectionRaw = 0f;
        Jump = false;
        Sprint = false;
        Mouse1 = false;
        Mouse2 = false;
        Mouse1Down = false;
        Mouse2Down = false;
        Mouse1Up = false;
        Mouse2Up = false;
        Shift = false;
        Special = false;
        ActiveItem = false;
    }
}
