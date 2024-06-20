using System.Collections;
using UnityEngine;

public class Bubble : Weapon
{
	public Rigidbody2D rb2d;

	private int _wallLayer;
	private int _hits;
	private float _timer;
	private Vector2 _direction;

	public override void SetDefaults()
	{
		base.SetDefaults();

		_wallLayer = LayerMask.NameToLayer( "Wall" );
	}

	protected override void StartAttackInternal()
	{
		base.StartAttackInternal();

		_timer = 0f;
		_hits = 0;

		StartCoroutine( MoveCoroutine() );
	}

	private IEnumerator MoveCoroutine()
	{
		_direction = Direction;
		while ( true )
		{
			UpdateTimer();
			Move();
			yield return null;
		}
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.layer == _wallLayer )
		{
			DestroyWeaponObject();
			return;
		}

		if ( collision.gameObject.layer == _enemyLayer_ )
		{
			DoDamage();
			_hits++;
			if ( _hits > currentStats.pierce )
				DestroyWeaponObject();
		}
	}

	private void Move()
	{
		rb2d.velocity = _direction * currentStats.spd;
	}

	private void UpdateTimer()
	{
		_timer += Time.deltaTime;
		if ( _timer > currentStats.duration )
			DestroyWeaponObject();
	}
}
