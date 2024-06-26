using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using static GridUtility;
using stats = SectorStats;

public class Enemy : MonoBehaviour
{
	public FlowFieldController flowFieldController;
	public EnemyPoolController enemyPoolController;
	public EnemyStats currentStats;
	public Rigidbody2D rb2D;

	protected float2 _direction_;

	public void StartAttacking()
	{
		StartAttackingInternal();
	}

	protected virtual void StartAttackingInternal()
	{
		SetDirection();
		StartCoroutine( FindDirectionCoroutine() );
	}

	protected virtual void Update()
	{
		MoveInDirection();
	}

	protected void MoveInDirection()
	{
		rb2D.velocity = _direction_ * currentStats.spd;
	}

	protected IEnumerator FindDirectionCoroutine()
	{
		while ( true )
		{
			SetDirection();

			yield return new WaitForSeconds( 0.2f );
		}
	}

	protected void SetDirection()
	{
		if ( flowFieldController != null && flowFieldController.FlowField != null )
		{
			int2 cellIndex = GetIndexFromPosition(
				transform.position,
				flowFieldController.FlowField.GridOrigin,
				flowFieldController.FlowField.GridSize,
				stats.cellDiameter );

			object cellDirection = flowFieldController.FlowField.Cells[ cellIndex.x, cellIndex.y ].Direction;

			if ( cellDirection is Direction direction )
				_direction_ = ( float2 )direction;
			else if ( cellDirection is float2 float2 )
				_direction_ = float2;
		}
	}

	public void TakeDamage( float damage )
	{
		currentStats.hp -= math.max( damage - currentStats.def, damage * 0.15f );

		if ( currentStats.hp < 0 )
			DestroyEnemyobject();
	}

	protected void DestroyEnemyobject()
	{
		StopAllCoroutines();
		enemyPoolController.EnqueueEnemyObject( gameObject );
	}

}
