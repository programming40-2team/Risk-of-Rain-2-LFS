using TMPro;
using UnityEngine;

public class ItemContainerPriceUI : UI_Base
{
    public int itemPrice = -1;
    enum Texts
    {
        PriceText,
    }
    public override void Init()
    {

        Bind<TextMeshProUGUI>(typeof(Texts));
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        GetText((int)Texts.PriceText).text = $"{itemPrice}";
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponentInChildren<BoxCollider>().bounds.size.y);


    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
