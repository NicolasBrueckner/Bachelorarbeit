using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon
{
	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Update()
	{
		base.Update();
	}

	public void ScaleWeapon( float duration )
	{
		StartCoroutine( ScaleCoroutine( duration ) );
	}

	private IEnumerator ScaleCoroutine( float duration )
	{
		float timer = 0f;

		while ( timer < duration )
		{
			float ratio = timer / duration;
			transform.localScale = Vector3.Lerp( Vector3.zero, _size_, ratio );
			timer += Time.deltaTime;
			yield return null;
		}
	}
}
