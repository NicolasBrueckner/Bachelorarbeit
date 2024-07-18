using System.Collections;
using UnityEngine;

public class Bubble : Weapon
{
	public Rigidbody2D rb2D;

	private int _wallLayer;
	private int _hits;
	private bool _isBeingQueued;
	private Vector2 _direction;

	protected override void StartAttackInternal()
	{
		base.StartAttackInternal();

		_isBeingQueued = false;
		_hits = 0;

		StartCoroutine( MoveCoroutine() );
	}

	private IEnumerator MoveCoroutine()
	{
		_direction = Direction;
		rb2D.velocity = currentStats[ StatType.spd ] * Time.deltaTime * _direction;

		yield return new WaitForSeconds( currentStats[ StatType.duration ] );
		DestroyWeaponObject();
	}

	protected override void OnTriggerEnter2D( Collider2D collision )
	{
		base.OnTriggerEnter2D( collision );

		if ( !_isBeingQueued && ++_hits > currentStats[ StatType.pierce ] )
		{
			_isBeingQueued = true;
			DestroyWeaponObject();
		}
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.layer == _wallLayer )
			DestroyWeaponObject();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();

		_wallLayer = LayerMask.NameToLayer( "cost_impassable" );
	}

}
