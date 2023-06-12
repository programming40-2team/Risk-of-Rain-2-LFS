using UnityEngine;

public class Item1025Skill : ItemPrimitiive
{
    private int _teleCode = -1;
    public int TeleCode => _teleCode;
    Item1025Skill[] Item1025;

    private bool _isMoving = false;
    private Rigidbody _rigid;
    private MeshRenderer _ObjectMesh;
    private Vector3 _targetDir;
    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        if (_teleCode == 1)
        {
            gameObject.SetRandomPositionSphere(0, 1, 5, Player.transform);
        }
        else
        {
            Vector3 targetPos = gameObject.SetRandomPositionSphere(300, 500, 10, Player.transform);
            _targetDir = (targetPos - Player.transform.position).normalized;
            _rigid.velocity = 500 * _targetDir;
        }

    }
    public void SetTeleCode(int n)
    {
        _teleCode = n;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Define.LayerMask.Enviroment)
        {

        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Item1025 = FindObjectsOfType<Item1025Skill>();
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKeyDown(KeyCode.E) && !_isMoving)
            {
                _isMoving = true;
                for (int i = 0; i < Item1025.Length; i++)
                {
                    if (Item1025[i].TeleCode != _teleCode)
                    {
                        Debug.Log("내 텔레코드 : " + TeleCode);
                        Debug.Log("통과한 텔레코드 : " + Item1025[i].TeleCode);
                        Player.transform.position = Item1025[i].transform.position;
                        break;
                    }
                }


            }

            _isMoving = false;
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
        }
        else if (other.gameObject.layer == (int)Define.LayerMask.Enviroment)
        {
            _rigid.velocity = Vector3.zero;
        }


    }

}
