using UnityEngine;


public enum BOARD
{
    COMPACT,
    DETAIL,
    INVEN,
    MAX
}

//나중에 음.. JSON으로 읽을때 쓸듯?
public class SpriteInfo
{
    public Sprite sprite = null;
    public int size = 0;
}

[AddComponentMenu("NGUI/Examples/Drag and Drop Item (Example)")]
public class DragDropItemTal : UIDragDropItem
{
    UI2DSprite MySprite = null;
    UIWidget MyWidget = null;
    public Sprite Compact = null;
    public Sprite Detail = null;
    public int CompactSize = 0;
    public int DetailSize = 0;
    Transform PrevParent = null;
    Vector3 PrevPosition = Vector3.zero;
    SpriteInfo PrevSpriteInfo = new SpriteInfo();
    Camera MyUICamera = null;

    protected override void Start()
    {
       MySprite = this.GetComponent<UI2DSprite>();
       MyWidget = this.GetComponent<UIWidget>();
       MyUICamera = GameObject.Find("Camera").GetComponent<Camera>();
       base.Start();
    }

    protected override void OnPress(bool isPressed)
    {
        ItemManager.Instance.Description(this.gameObject.GetComponent<BaseItem>());
        base.OnPress(isPressed);
    }

    //추후 다른 방법으로 바꿔야함 모든 아이템에 적용할 수 있는 방법으로!
    protected override void OnDragDropStart()
    {
        PrevParent = this.transform.parent;
        PrevPosition = this.transform.localPosition;
        PrevSpriteInfo.sprite = MySprite.sprite2D;

        if (PrevSpriteInfo.sprite.name.Equals("bukler"))
        {
            PrevSpriteInfo.size = 100;
        }
        else
        {
            PrevSpriteInfo.size = 200;
        }
        base.OnDragDropStart();
    }

    protected override void OnDragDropMove(Vector2 delta)
    {
        mTrans.localPosition += mTrans.InverseTransformDirection((Vector3)delta);
        GameObject surface = UICamera.hoveredObject;
        if (surface != null)
        {
            ExampleDragDropSurface dds = surface.GetComponent<ExampleDragDropSurface>();

            if (dds != null)
            {
                MySprite.MakePixelPerfect();
                if (dds.gameObject.name.Equals("ItemDetail"))
                {
                    ChangeSprite(Detail, DetailSize);
                }
                else if (dds.gameObject.name.Equals("BackGround") || dds.gameObject.name.Contains("Blank"))
                {
                    ChangeSprite(Compact, CompactSize);
                }

                Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                this.transform.position = MyUICamera.ViewportToWorldPoint(mousePos);
            }
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        if (surface != null)
        {
            ExampleDragDropSurface dds = surface.GetComponent<ExampleDragDropSurface>();

            if (dds != null)
            {
                this.transform.SetParent(dds.transform);
                if (dds.gameObject.name.Contains("Blank"))
                {
                    this.transform.localPosition = Vector3.zero;
                }
                else if (dds.gameObject.name.Contains("BackGround"))
                {
                    RestoreItem();
                }
            }
            else if (surface.GetComponent<BaseItem>() != null)
            {
                base.OnDragDropRelease(surface);
            }
            else
            {
                RestoreItem();
            }
        }
        this.GetComponent<Collider>().enabled = true;
    }
    
    //void IsAllowItem()
    //{

    //}

    void RestoreItem()
    {
        this.transform.SetParent(PrevParent);
        this.transform.localPosition = PrevPosition;
        ChangeSprite(PrevSpriteInfo.sprite, PrevSpriteInfo.size);
    }

    void ChangeSprite(Sprite pSprite, int pObject)
    {
        MySprite.sprite2D = pSprite;
        MySprite.width = pObject;
    }
}
