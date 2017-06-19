using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoSingleton<ActorManager> 
{
    const float MOVETIME = 2.0f;

    GameObject UICamera = null;
    GameObject PrefabPlayer = null;
    GameObject CurrentPlayer = null;
    bool IsOnPlayer = false;
    bool IsMovePlayer = false;
    float dTime = 0f;
    Vector3 StartPosition = Vector3.zero;
    Vector3 EndPosition = Vector3.zero;

    void Start () 
	{
		StartPosition = GameObject.Find("MercenaryStartPosition").transform.position;
		EndPosition = GameObject.Find("MercenaryEndPosition").transform.position;

		UICamera = GameObject.Find("UICamera");
        PrefabPlayer = Resources.Load("Player") as GameObject;
        //CreateUIPlayer();
    }


    private void Update()
    {
        if (IsMovePlayer)
        {
            dTime += Time.deltaTime;
			CurrentPlayer.transform.position = Vector3.Lerp(StartPosition, EndPosition, dTime / MOVETIME);

            if (dTime > MOVETIME)
            {
                dTime = 0;
                IsMovePlayer = false;
				CurrentPlayer.GetComponent<Actor>().ThrowItem();
            }
        }
    }
	
    public void CreateUIPlayer()
    {
		if (IsOnPlayer) return;

		CurrentPlayer = NGUITools.AddChild(UICamera.gameObject, PrefabPlayer);
		CurrentPlayer.transform.position = StartPosition;
		IsMovePlayer = true;
		IsOnPlayer = true;
	}

	public void PassPlayer()
	{
		//ActorManager 플레이어 생성부분 초기화하고
		//전투씬&뷰에 캐릭터 배치
	}

	public void DropPlayer()
	{
		//ActorManager 플레이어 생성부분 초기화하고
		//캐릭터 그냥 나감
	}
}
