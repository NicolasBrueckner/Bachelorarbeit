using System.Collections;
using UnityEngine;

public class Fist : Weapon
{
	public override void SetDefaults()
	{
		base.SetDefaults();
	}

	protected override void StartAttackInternal()
	{
		base.StartAttackInternal();

		ScaleFist();
	}

	protected override void OnTriggerEnter2D( Collider2D collision )
	{
		base.OnTriggerEnter2D( collision );
	}

	private void ScaleFist()
	{
		RotateToDirection();
		StartCoroutine( ScaleFistCoroutine() );
	}

	private IEnumerator ScaleFistCoroutine()
	{
		float timer = 0f;

		while ( timer < currentStats.duration )
		{
			float ratio = timer / currentStats.spd;
			_transform_.localScale = Vector3.Lerp( Vector3.zero, Size, ratio );
			timer += Time.deltaTime;

			yield return null;
		}

		_transform_.localScale = Vector3.zero;
		DestroyWeaponObject();
	}
}
