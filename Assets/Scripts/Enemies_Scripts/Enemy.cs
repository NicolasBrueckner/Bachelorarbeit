using EditorAttributes;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using static GridUtility;
using stats = SectorStats;

public class Enemy : MonoBehaviour
{
	[ReadOnly]
	public Stats currentStats;

	public float currentHP;
	public Rigidbody2D rb2D;
	public Transform targetTransform;
	public FlowFieldController flowFieldController;
	public EnemyPoolController enemyPoolController;

	protected Vector2 _direction_;

	public void StartAttack() => StartAttackInternal();

	protected virtual void StartAttackInternal()
	{
		StartCoroutine( FindDirectionCoroutine() );
		StartCoroutine( CheckDistanceCoroutine() );
	}

	protected virtual void FixedUpdate()
	{
		MoveInDirection();
	}

	protected void MoveInDirection()
	{
		rb2D.velocity = _direction_ * currentStats[ StatType.spd ] * Time.deltaTime;
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
			if ( math.distance( transform.position, targetTransform.position ) > 20f )
				DestroyEnemyobject( false );

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
				_direction_ = ( Vector2 )direction;
			else if ( cellDirection is Vector2 trueDirection )
				_direction_ = trueDirection;
		}
	}

	public void TakeDamage( float damage )
	{
		currentHP -= math.max( damage - currentStats[ StatType.def ], damage * 0.15f );

		if ( currentHP < 0 )
			DestroyEnemyobject( true );
	}

	protected void DestroyEnemyobject( bool wasKilled )
	{
		StopAllCoroutines();
		currentHP = currentStats[ StatType.max_hp ];
		enemyPoolController.EnqueueEnemyObject( gameObject, wasKilled );
	}

	public void RescaleObject()
	{
		transform.localScale = Vector3.one * currentStats[ StatType.size ];
	}
}
