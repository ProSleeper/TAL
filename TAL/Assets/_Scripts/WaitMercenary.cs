using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ANIMSTATE
{
	IDLE,
	WALK,
	MAX
}


public class WaitMercenary : MonoBehaviour 
{
	float MOVETIME = 3.0f;

	Animator Anim = null;
	bool IsMove = false;
	ANIMSTATE CurrentState = ANIMSTATE.IDLE;
	float dTime = 0f;

	Vector3 StartPos = Vector3.zero;
	Vector3 EndPos = Vector3.zero;

	private void Awake()
	{
		Anim = this.transform.GetChild(0).GetComponent<Animator>();
	}
	
	private void Update()
	{
		if (IsMove)
		{
			dTime += Time.deltaTime;
			this.transform.position = Vector3.Lerp(StartPos, EndPos, dTime / MOVETIME);
			if (dTime > MOVETIME)
			{
				MoveStateChange();
			}
		}
	}

	//상태만 바꿈
	public void MoveStateChange()
	{
		IsMove = !IsMove;

		int state = (CurrentState == ANIMSTATE.IDLE ? 1 : 0);
		CurrentState = (ANIMSTATE)state;
		Anim.SetInteger("State", state);
	}
	
	public void TargetMove(Vector3 pPos, float pTime)
	{
		StartPos = this.transform.position;
		EndPos = pPos;
		MOVETIME = pTime;
		dTime = 0;
		MoveStateChange();
	}


}
