using EditorAttributes;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[ReadOnly]
	public WeaponController controller;
	[ReadOnly]
	public WeaponStats currentStats;

	protected Vector2 _direction_;
	protected Vector3 _size_;

	protected Transform _transform_;

	protected virtual void Awake()
	{
		_transform_ = transform;
	}

	public void ScaleToSize()
	{
		_size_ = Vector3.one * currentStats.size;
		_transform_.localScale = _size_;
	}

	public void RotateToDirection()
	{
		_direction_ = controller.Direction;
		_transform_.rotation = Quaternion.LookRotation( Vector3.forward, _direction_ );
	}
}
