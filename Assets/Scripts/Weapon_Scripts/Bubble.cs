using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Weapon
{
	public Rigidbody2D rb2d;

	private float _timer;
	private int _hits;

	protected override void Awake()
	{
		base.Awake();
	}

	private void OnEnable()
	{
		_direction_ = controller?.Direction ?? Vector2.zero;
		_timer = 0f;
		_hits = 0;
	}

	protected override void Update()
	{
		UpdateTimer();
		Move();
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		_hits++;
		if ( _hits > currentStats.pierce )
			DestroyBubble();
	}

	private void Move()
	{
		rb2d.velocity = _direction_ * currentStats.spd;
	}

	private void UpdateTimer()
	{
		_timer += Time.deltaTime;
		if ( _timer > currentStats.duration )
			DestroyBubble();
	}

	private void DestroyBubble()
	{
		( controller as BubbleController ).ReEnqueueBubble( gameObject );
	}
}
