using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header( "Stats" )]
	public float maxHP;
	public float atk;
	public float def;
	public float spd;

	public Rigidbody2D rb2D;

	protected float2 direction;

	private void Update()
	{
		MoveInDirection();
	}

	protected void MoveInDirection()
	{
		SetDirection();
		rb2D.velocity = direction * spd * Time.deltaTime;
	}

	protected void SetDirection()
	{

	}

}
