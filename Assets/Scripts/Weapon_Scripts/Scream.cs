using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : Weapon
{
	private HashSet<Collider2D> _collidersInTrigger = new();

	public override void SetDefaults()
	{
		base.SetDefaults();
	}

	protected override void StartAttackInternal()
	{
		base.StartAttackInternal();

		StartCoroutine( DamageCoroutine() );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.layer == _enemyLayer_ )
		{
			_collidersInTrigger.Add( collision );
		}
	}

	private void OnTriggerExit2D( Collider2D collision )
	{
		_collidersInTrigger.Remove( collision );
	}

	private IEnumerator DamageCoroutine()
	{
		while ( true )
		{
			yield return new WaitForSeconds( currentStats.atk_spd );

			foreach ( Collider2D collider in _collidersInTrigger )
			{
				DoDamage();
			}
		}
	}
}
