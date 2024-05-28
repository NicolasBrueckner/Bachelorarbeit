using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static GridUtility;

public class Enemy : MonoBehaviour
{
	[Header( "Stats" )]
	public float maxHP;
	public float atk;
	public float def;
	public float spd;

	public Rigidbody2D rb2D;
	public FlowFieldController flowFieldController;

	protected float _currentHP_;
	protected float2 _direction_;

	protected virtual void Awake()
	{
		flowFieldController = FlowFieldController.Instance;
	}

	protected virtual void OnEnable()
	{
		SetDirection();
		StartCoroutine( ChangeDirection() );
	}

	protected virtual void OnDisable()
	{
		StopCoroutine( ChangeDirection() );
	}

	protected virtual void Update()
	{
		MoveInDirection();
	}

	protected void MoveInDirection()
	{
		rb2D.velocity = _direction_ * spd * Time.deltaTime;
	}

	protected IEnumerator ChangeDirection()
	{
		while ( true )
		{
			SetDirection();

			yield return new WaitForSeconds( 0.2f );
		}
	}

	protected void SetDirection()
	{
		if ( flowFieldController.FlowField != null )
		{
			int2 cellIndex = GetIndexFromPosition(
				transform.position,
				flowFieldController.FlowField.GridOrigin,
				flowFieldController.FlowField.GridSize,
				Sector.cellDiameter );

			object cellDirection = flowFieldController.FlowField.Cells[ cellIndex.x, cellIndex.y ].Direction;

			if ( cellDirection is Direction direction )
				_direction_ = ( float2 )direction;
			else if ( cellDirection is float2 float2 )
				_direction_ = float2;
		}
	}

}
