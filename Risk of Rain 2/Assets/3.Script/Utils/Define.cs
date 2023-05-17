public class Define
{
    public enum Scene
    {
        None,
        MainMenu,
       

    }
    public enum UIEvent
    {
        Click,
        OnDrag,
        PointerEnter,
        PointerExit,
        OnBeginDrag,
        OnEndDrag,
        OnDrop

    }
    public enum ItemType
    {
       Passive,
       Active,

    }
    public enum EVENT_TYPE
    {
        StartBattle,
        EndBattle,
        PlayerHp,
        EnemyHp,
        
    }
    public enum WhenItemActivates
    {
        Always,
        AfterBattle,
        InBattle,
        NotBattle,

    }
}
