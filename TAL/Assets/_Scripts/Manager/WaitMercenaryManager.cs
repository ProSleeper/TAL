using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitMercenaryManager : MonoSingleton<WaitMercenaryManager> 
{
	const float INITMOVETIME = 1.0f;
	const int MAXCHARACTERCOUNT = 15;

	public Transform SpawnStartPos = null;
	public Transform SpawnEndPos = null;


	List<GameObject> KnightList = new List<GameObject>();
	GameObject[] KnightPrefab = null;
	Transform FrontKnight;
	Vector3 nextPos = Vector3.zero;
	public bool IsOnClick = true;

	public UIButton ComeOnButton = null;

	private void Awake()
	{
		KnightPrefab = Resources.LoadAll<GameObject>("Character");
		ComeOnButton.onClick.Add(new EventDelegate(this, "ComeOnKnight"));
	}

	void Start () 
	{
		CreateCharacter();
	}

	private void Update()
	{
	}

	public void ControlWaitCharacter()
	{
		for (int i = 0; i < KnightList.Count; i++)
		{
			KnightList[i].GetComponent<WaitMercenary>().TargetMove(nextPos, Random.Range(0.5f, 1.0f));

			
			nextPos = KnightList[i].transform.position;
		}
		FrontKnight = null;
	}

	public void StopWaitCharacter()
	{
		foreach (GameObject knight in KnightList)
		{
			if (FrontKnight == null)
			{
				FrontKnight = knight.transform;
				continue;
			}

			if (FrontKnight.transform.position.x < knight.transform.position.x)
			{
				FrontKnight = knight.transform;
			}
		}
		KnightList.Remove(FrontKnight.gameObject);
	}

	public void ComeOnKnight()
	{
		if (IsOnClick)
		{
			StopWaitCharacter();
			nextPos = FrontKnight.transform.position;
			FrontKnight.GetComponent<WaitMercenary>().TargetMove(FrontKnight.transform.position + new Vector3(2, 0, 0), 3.0f);
			Invoke("CreateUI", 2.0f);
			Invoke("ControlWaitCharacter", 2.0f);
			IsOnClick = false;
		}
	}

	void CreateUI()
	{
		ActorManager.Instance.CreateUIPlayer();
	}

	void CreateCharacter()
	{
		for (int i = 0; i < MAXCHARACTERCOUNT; i++)
		{
			GameObject OnKnight = Instantiate(KnightPrefab[Random.Range(0, KnightPrefab.Length)]);
			OnKnight.transform.position = new Vector3(Random.Range(SpawnStartPos.position.x, SpawnEndPos.position.x), SpawnStartPos.position.y, SpawnStartPos.position.z);
			KnightList.Add(OnKnight);
		}

		KnightList.Sort(((GameObject lhs, GameObject rhs) => rhs.transform.position.x.CompareTo(lhs.transform.position.x)));

	}
}
