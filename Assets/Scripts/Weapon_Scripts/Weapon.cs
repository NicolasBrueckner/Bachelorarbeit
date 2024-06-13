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
	protected Vector3 _size_;

	protected virtual void Awake()
	{
		currentStats = new( baseStats );

		ScaleToSize();
	}

	protected virtual void Update()
	{
		_direction_ = controller.Direction;
		RotateToDirection();
	}

	private void ScaleToSize()
	{
		_size_ = Vector3.one * currentStats.size;
		transform.localScale = _size_;
	}

	private void RotateToDirection()
	{
		transform.rotation = Quaternion.LookRotation( Vector3.forward, _direction_ );
	}
}
