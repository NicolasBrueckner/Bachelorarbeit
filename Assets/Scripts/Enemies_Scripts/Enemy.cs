using EditorAttributes;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using static GridUtility;
using stats = SectorStats;

public class Enemy : MonoBehaviour
{
	[ReadOnly]
	public EnemyStats currentStats;

	public Rigidbody2D rb2D;
	public FlowFieldController flowFieldController;
	public EnemyPoolController enemyPoolController;

	protected float2 _direction_;

	public void StartAttack()
	{
		StartAttackInternal();
	}

	protected virtual void StartAttackInternal()
	{
		StartCoroutine( FindDirectionCoroutine() );
		StartCoroutine( CheckDistanceCoroutine() );
	}

	protected virtual void Update()
	{
		MoveInDirection();
	}

	protected void MoveInDirection()
	{
		rb2D.velocity = _direction_ * currentStats.spd * Time.deltaTime;
	}

	protected IEnumerator FindDirectionCoroutine()
	{
		while ( gameObject.activeInHierarchy )
		{
			SetDirection();

			yield return new WaitForSeconds( 0.2f );
		}
	}

	protected IEnumerator CheckDistanceCoroutine()
	{
		while ( gameObject.activeInHierarchy )
		{
			if ( math.distance( transform.position, enemyPoolController.targetObject.transform.position ) > 20f )
				DestroyEnemyobject();

			yield return new WaitForSeconds( 1f );
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
