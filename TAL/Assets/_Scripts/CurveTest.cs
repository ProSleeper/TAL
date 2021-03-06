﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTest : MonoBehaviour 
{
	const float MOVETIME = 10.0f;

	public GameObject[] sphere;

	public Vector3 StartPos = Vector3.zero;
	public Vector3 MiddlePos = Vector3.zero;
	public Vector3 EndPos = Vector3.zero;

	float dTime = 0;

	void Start () 
	{
		StartPos = this.transform.position;
		MiddlePos = sphere[0].transform.position;
		EndPos = sphere[1].transform.position;
		this.GetComponentInChildren<Animator>().SetInteger("State", 1);
	}



	private void Update()
	{
		dTime += Time.deltaTime;

		Vector3 firstPos = Vector3.Lerp(StartPos, MiddlePos, dTime / MOVETIME);
		Vector3 secondPos = Vector3.Lerp(MiddlePos, EndPos, dTime / MOVETIME);

		this.transform.position = Vector3.forward;
		this.transform.position = Vector3.Lerp(firstPos, secondPos, dTime / MOVETIME);
	}


}
