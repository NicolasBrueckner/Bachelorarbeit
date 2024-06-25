using System.Collections;
using UnityEngine;

public class Bubble : Weapon
{
	public Rigidbody2D rb2d;

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

		_hits = 0;

		StartCoroutine( MoveCoroutine() );
	}

	private IEnumerator MoveCoroutine()
	{
		_direction = Direction;
		rb2d.velocity = _direction * currentStats.spd;

		yield return new WaitForSeconds( currentStats.duration );
		DestroyWeaponObject();
	}

	protected override void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.layer == _wallLayer )
			DestroyWeaponObject();

		base.OnTriggerEnter2D( collision );

		if ( ++_hits > currentStats.pierce )
			DestroyWeaponObject();
	}
}
