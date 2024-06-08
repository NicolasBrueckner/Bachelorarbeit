using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BubbleController : WeaponController
{
	public float duration;

	private Queue<GameObject> _bubbles = new();

	protected override void Start()
	{
		base.Start();

		InitializeObjectPool( 10 );
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void Attack()
	{
		base.Attack();

	}

	private void InitializeObjectPool( int poolSize )
	{
		for ( int i = 0; i < poolSize; i++ )
		{
			GameObject bubbleObject = Instantiate( weaponObject );
			bubbleObject.SetActive( false );
			_bubbles.Enqueue( bubbleObject );
		}
	}
}
