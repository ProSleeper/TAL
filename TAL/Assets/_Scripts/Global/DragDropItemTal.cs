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
    /// <summary>
    /// Prefab object that will be instantiated on the DragDropSurface if it receives the OnDrop event.
    /// </summary>
    /// 
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


    /// <summary>
    /// Drop a 3D game object onto the surface.
    /// </summary>

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
        GameObject surface =  UICamera.hoveredObject;
        Debug.Log(surface.name);
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
                else if (dds.gameObject.name.Equals("Inventory") || dds.gameObject.name.Equals("Blank"))
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
                if (dds.gameObject.name.Equals("Blank"))
                {
                    this.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                this.transform.SetParent(PrevParent);
                this.transform.localPosition = PrevPosition;
                ChangeSprite(PrevSpriteInfo.sprite, PrevSpriteInfo.size);
            }
        }
        this.GetComponent<Collider>().enabled = true;
        //base.OnDragDropRelease(surface);
    }


    void ChangeSprite(Sprite pSprite, int pObject)
    {
        MySprite.sprite2D = pSprite;
        MySprite.width = pObject;
    }
}
