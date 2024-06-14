using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon
{
	protected override void Awake()
	{
		base.Awake();
	}

	public void ScaleFist()
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
			_transform_.localScale = Vector3.Lerp( Vector3.zero, _size_, ratio );
			timer += Time.deltaTime;

			yield return null;
		}

		_transform_.localScale = Vector3.zero;
	}
}
