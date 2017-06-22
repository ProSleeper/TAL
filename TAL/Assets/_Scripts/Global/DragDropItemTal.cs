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
	const int ClickItemDepth = 10;

	UI2DSprite MySprite = null;
	//UIWidget MyWidget = null;
	public Sprite Compact = null;
	public Sprite Detail = null;
	public int CompactSize = 0;
	public int DetailSize = 0;

	//클릭시 뎁스 체크할 변수
	//int PrevDepth = 0;
	Transform PrevParent = null;
	Vector3 PrevPosition = Vector3.zero;
	SpriteInfo PrevSpriteInfo = new SpriteInfo();
	Camera MyUICamera = null;


	//뎁스판단을 어떻게 할지 고민한 결과..
	//뎁스 판단은 Detail에서만 한다. 
	//그러므로 Detail에 있는 
	//모든 아이템끼리(그래봐야 몇개 안될듯) 뎁스 판단 후
	//제일 높은 뎁스 보다 +1을 해주는 방향으로 해보자
	protected override void Start()
	{
		MySprite = this.GetComponent<UI2DSprite>();
		//MyWidget = this.GetComponent<UIWidget>();
		MyUICamera = GameObject.Find("UICamera").GetComponent<Camera>();
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

	//원래 코드
	//protected override void OnDragDropMove(Vector2 delta)
	//{
	//    mTrans.localPosition += mTrans.InverseTransformDirection((Vector3)delta);
	//    GameObject surface = UICamera.hoveredObject;
	//    if (surface != null)
	//    {
	//        ExampleDragDropSurface dds = surface.GetComponent<ExampleDragDropSurface>();

	//        if (dds != null)
	//        {
	//            MySprite.MakePixelPerfect();
	//            if (dds.gameObject.name.Equals("ItemDetail"))
	//            {
	//                ChangeSprite(Detail, DetailSize);
	//            }
	//            else if (dds.gameObject.name.Equals("BackGround") || dds.gameObject.name.Contains("Blank"))
	//            {
	//                ChangeSprite(Compact, CompactSize);
	//            }

	//            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	//            this.transform.position = MyUICamera.ViewportToWorldPoint(mousePos);
	//        }
	//    }
	//}

	//protected override void OnDragDropRelease(GameObject surface)
	//{
	//    if (surface != null)
	//    {
	//        ExampleDragDropSurface dds = surface.GetComponent<ExampleDragDropSurface>();

	//        if (dds != null)
	//        {
	//            this.transform.SetParent(dds.transform);
	//            if (dds.gameObject.name.Contains("Blank"))
	//            {
	//                this.transform.localPosition = Vector3.zero;
	//            }
	//            else if (dds.gameObject.name.Contains("BackGround"))
	//            {
	//                RestoreItem();
	//            }
	//        }
	//        else if (surface.GetComponent<BaseItem>() != null)
	//        {
	//            base.OnDragDropRelease(surface);
	//        }
	//        else
	//        {
	//            RestoreItem();
	//        }
	//    }

	//    this.GetComponent<Collider>().enabled = true;
	//}

	//void IsAllowItem()
	//{

	//}

	protected override void OnDragDropMove(Vector2 delta)
	{
		mTrans.localPosition += mTrans.InverseTransformDirection((Vector3)delta);
		GameObject surface = UICamera.hoveredObject;

		ItemDropArea dropArea = surface.GetComponent<ItemDropArea>();

		if (dropArea != null)
		{
			//MySprite.MakePixelPerfect();
			if (dropArea.CURRENTAREA == AREA.DETAIL)
			{
				ChangeSprite(Detail, DetailSize);
			}
			else if (dropArea.CURRENTAREA == AREA.INVENBACK || dropArea.CURRENTAREA == AREA.INVEN)
			{
				ChangeSprite(Compact, CompactSize);
			}

			Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			this.transform.position = MyUICamera.ViewportToWorldPoint(mousePos);
		}
	}

	protected override void OnDragDropRelease(GameObject surface)
	{
		if (surface != null)
		{
			ItemDropArea dropArea = surface.GetComponent<ItemDropArea>();

			if (dropArea != null)
			{
				this.transform.SetParent(dropArea.transform);
				if (dropArea.CURRENTAREA == AREA.INVEN && dropArea.transform.childCount < 2)
				{
					this.transform.localPosition = Vector3.zero;
				}
				else if (dropArea.CURRENTAREA == AREA.DETAIL)
				{

				}
				else
				{
					RestoreItem();
				}
			}
			else if (surface.GetComponent<BaseItem>() != null)
			{
				if (MySprite.sprite2D == Compact)
				{
					RestoreItem();
				}
				base.OnDragDropRelease(surface);
			}
			else
			{
				RestoreItem();
			}
		}

		this.GetComponent<Collider>().enabled = true;
	}

	void RestoreItem()
	{
		this.transform.SetParent(PrevParent);
		this.transform.localPosition = PrevPosition;
		ChangeSprite(PrevSpriteInfo.sprite, PrevSpriteInfo.size);
	}

	void ChangeSprite(Sprite pSprite, int pObjectWidth)
	{
		MySprite.sprite2D = pSprite;
		MySprite.width = pObjectWidth;
	}
}
