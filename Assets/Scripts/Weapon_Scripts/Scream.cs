using System.Collections;
using UnityEngine;

public class Scream : Weapon
{
	public override void SetDefaults()
	{
		base.SetDefaults();
	}

	protected override void StartAttackInternal()
	{
		base.StartAttackInternal();

		StartCoroutine( CollisionCoroutine() );
	}

	protected override void OnTriggerEnter2D( Collider2D collision )
	{
		base.OnTriggerEnter2D( collision );
	}

	private IEnumerator CollisionCoroutine()
	{
		while ( gameObject.activeInHierarchy )
		{
			_collider_.enabled = true;
			yield return new WaitForEndOfFrame();

			_collider_.enabled = false;
			yield return new WaitForSeconds( currentStats[ StatType.atk_spd ] );
		}
	}
}
