using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BubbleController : WeaponController
{
	private Queue<GameObject> _bubbles = new();

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void Attack()
	{
		base.Attack();

		if ( _bubbles.Count > 0 )
		{
			GameObject dequeuedBubble = _bubbles.Dequeue();
			dequeuedBubble.SetActive( true );
		}
	}

	public void EnqueueBubble( GameObject bubble )
	{
		bubble.SetActive( false );
		bubble.transform.position = transform.position;
		_bubbles.Enqueue( bubble );
	}

	protected override void InitializeWeapon()
	{
		base.InitializeWeapon();

		for ( int i = 0; i < 10; i++ )
		{
			GameObject bubbleObject = Instantiate( weaponObject, transform );
			Bubble bubble = bubbleObject.GetComponent<Bubble>();

			InitializeWeaponObject( bubbleObject, bubble );
			_bubbles.Enqueue( bubbleObject );
		}
	}
}
