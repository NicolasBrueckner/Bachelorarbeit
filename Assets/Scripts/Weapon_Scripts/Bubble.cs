using System.Collections;
using UnityEngine;

public class Bubble : Weapon
{
	public Rigidbody2D rb2d;

	private bool _isBeingQueued;
	private int _wallLayer;
	private int _hits;
	private Vector2 _direction;

	public override void SetDefaults()
	{
		base.SetDefaults();

		_wallLayer = LayerMask.NameToLayer( "Wall" );
	}

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
		rb2d.velocity = _direction * currentStats[ StatType.spd ];

		yield return new WaitForSeconds( currentStats[ StatType.duration ] );
		DestroyWeaponObject();
	}

	protected override void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.layer == _wallLayer )
			DestroyWeaponObject();

		base.OnTriggerEnter2D( collision );

		if ( !_isBeingQueued && ++_hits > currentStats[ StatType.pierce ] )
		{
			_isBeingQueued = true;
			DestroyWeaponObject();
		}
	}
}
