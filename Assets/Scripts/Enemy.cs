using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
	[Header( "Stats" )]
	public float maxHP;
	public float atk;
	public float def;
	public float spd;

	public Rigidbody2D rb2D;

	protected float _currentHP_;
	protected float2 _direction_;

	protected virtual void OnEnable()
	{
		SetDirection();
	}

	protected virtual void Update()
	{
		MoveInDirection();
	}

	protected void MoveInDirection()
	{
		//SetDirection();
		rb2D.velocity = _direction_ * spd * Time.deltaTime;
	}

	protected void SetDirection()
	{
		_direction_ = math.normalize( new float2( Random.Range( -1f, 1f ), Random.Range( -1f, 1f ) ) );
	}

}
