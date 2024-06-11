using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public WeaponBaseStats baseStats;
	public WeaponStats currentStats;

	[HideInInspector]
	public WeaponController controller;

	protected Vector2 _direction_;

	protected virtual void Awake()
	{
		currentStats = new( baseStats );
	}

	protected virtual void Update()
	{
		_direction_ = controller.Direction;
		RotateToDirection();
	}

	private void RotateToDirection()
	{
		transform.rotation = Quaternion.LookRotation( Vector3.forward, _direction_ );
	}
}
