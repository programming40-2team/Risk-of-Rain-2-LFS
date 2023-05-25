using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    DataManager _data = new DataManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    ItemInventoryManager _iteminven = new ItemInventoryManager();
    EventManager _event = new EventManager();
    GameManager _game = new GameManager();
    public static DataManager Data { get { return Instance._data; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static ItemInventoryManager ItemInventory { get { return Instance._iteminven; } }
    public static EventManager Event { get { return Instance._event; } }

    public static GameManager Game { get { return Instance._game; } }


    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            //초기화가 필요한 멤버들의 초기화를 진행해 줍니다.
           //데이터 관련은 첫 씬인 MenMenu에서
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        UI.Clear();

    }
}
